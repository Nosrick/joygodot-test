using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CodingSeb.ExpressionEvaluator;
using JoyLib.Code.Helpers;
using JoyLib.Code.Unity.GUI;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace JoyLib.Code.Scripting
{
    public class ScriptingEngine
    {
        private static readonly Lazy<ScriptingEngine> LAZY = new Lazy<ScriptingEngine>(() => new ScriptingEngine());

        public static ScriptingEngine Instance => LAZY.Value;

        protected Assembly m_ScriptDLL;

        protected List<Type> m_Types;

        protected ExpressionEvaluator Eval;

        public ScriptingEngine()
        {
            if (this.m_Types is null)
            {
                try
                {
                    if (GlobalConstants.IS_EDITOR)
                    {
                        string dir = Directory.GetCurrentDirectory() + "/" + GlobalConstants.SCRIPTS_FOLDER;
                        string[] scriptFiles = Directory.GetFiles(dir, "*.cs", SearchOption.AllDirectories);

                        List<SyntaxTree> builtFiles = new List<SyntaxTree>();

                        foreach (string scriptFile in scriptFiles)
                        {
                            string contents = File.ReadAllText(scriptFile);
                            SyntaxTree builtFile = CSharpSyntaxTree.ParseText(contents);
                            builtFiles.Add(builtFile);
                        }

                        GlobalConstants.ActionLog.Log("Loaded " + scriptFiles.Length + " script files.");
                        List<MetadataReference> libs = new List<MetadataReference>
                        {
                            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                            MetadataReference.CreateFromFile(typeof(GlobalConstants).Assembly.Location),
                            MetadataReference.CreateFromFile(typeof(Queue<bool>).Assembly.Location),
                            MetadataReference.CreateFromFile(typeof(IQueryable).Assembly.Location),
                            MetadataReference.CreateFromFile(typeof(Castle.Core.Internal.CollectionExtensions).Assembly
                                .Location)
                        };
                        CSharpCompilation compilation = CSharpCompilation.Create("JoyScripts", builtFiles, libs,
                            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

                        MemoryStream memory = new MemoryStream();
                        EmitResult result = compilation.Emit(memory);

                        if (result.Success == false)
                        {
                            foreach (var diagnostic in result.Diagnostics)
                            {
                                if (diagnostic.Severity != DiagnosticSeverity.Error)
                                {
                                    continue;
                                }

                                GlobalConstants.ActionLog.Log(diagnostic.Severity.ToString(), LogLevel.Error);
                                GlobalConstants.ActionLog.Log(diagnostic.GetMessage(), LogLevel.Error);
                                GlobalConstants.ActionLog.Log(diagnostic.Location.ToString(), LogLevel.Error);
                            }
                        }

                        memory.Seek(0, SeekOrigin.Begin);
                        this.m_ScriptDLL = Assembly.Load(memory.ToArray());

                        //this.m_Types = new List<Type>(this.m_ScriptDLL.GetTypes());
                        this.m_Types = new List<Type>(typeof(IGUIManager).Assembly.GetExportedTypes());
                        //this.m_Types.AddRange(typeof(IJoyObject).Assembly.GetExportedTypes());
                    }
                    else
                    {
                        this.m_ScriptDLL = typeof(IGUIManager).Assembly;
                        this.m_Types = new List<Type>(this.m_ScriptDLL.GetExportedTypes());
                    }

                    this.Eval = new ExpressionEvaluator
                    {
                        OptionForceIntegerNumbersEvaluationsAsDoubleByDefault = true
                    };
                }
                catch (Exception ex)
                {
                    GlobalConstants.ActionLog.StackTrace(ex);
                }
            }
        }

        public object FetchAndInitialise(string type)
        {
            try
            {
                Type directType = this.m_Types.FirstOrDefault(t => t.Name.Equals(type, StringComparison.OrdinalIgnoreCase));

                if (directType is null)
                {
                    throw new Exception();
                }
                
                return Activator.CreateInstance(directType);
            }
            catch (Exception ex)
            {
                GlobalConstants.ActionLog.StackTrace(ex);
                GlobalConstants.ActionLog.Log("Error when searching for Type in ScriptingEngine, type name " + type, LogLevel.Error);
                return default;
            }
        }

        public T Initialise<T>()
        {
            try
            {
                return Activator.CreateInstance<T>();
            }
            catch (Exception e)
            {
                GlobalConstants.ActionLog.StackTrace(e);
                GlobalConstants.ActionLog.Log("Error when activating type " + nameof(T), LogLevel.Error);
                return default;
            }
        }

        public IEnumerable<T> FetchAndInitialiseChildren<T>()
        {
            try
            {
                Type[] types = this.m_Types.Where(t => typeof(T).IsAssignableFrom(t) && t.IsAbstract == false).ToArray();
                List<T> children = new List<T>();
                foreach (Type tempType in types)
                {
                    children.Add((T) Activator.CreateInstance(tempType));
                }

                return children;
            }
            catch (Exception e)
            {
                GlobalConstants.ActionLog.StackTrace(e);
                GlobalConstants.ActionLog.Log("Error when searching for Type in ScriptingEngine, type name " + nameof(T), LogLevel.Error);
                return default;
            }
        }

        public IEnumerable<Type> FetchTypeAndChildren(string typeName)
        {
            try
            {
                Type directType = this.m_Types.FirstOrDefault(type => type.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase));

                List<Type> children = new List<Type>();
                if (directType is null == false)
                {
                    children = this.m_Types.Where(type => directType.IsAssignableFrom(type)).ToList();
                }
                else
                {
                    directType = Type.GetType(typeName, true, true);
                    children = this.m_Types.Where(type => directType.IsAssignableFrom(type)).ToList();
                    children = children.Where(t => t.IsAbstract == false && t.IsInterface == false).ToList();
                }

                return children;
            }
            catch (Exception ex)
            {
                GlobalConstants.ActionLog.StackTrace(ex);
                GlobalConstants.ActionLog.Log("Error when searching for Type in ScriptingEngine, type name " + typeName, LogLevel.Error);
                return default;
            }
        }

        public IEnumerable<Type> FetchTypeAndChildren(Type type)
        {
            try
            {
                Type[] types = this.m_Types.Where(t => type.IsAssignableFrom(t) && t.IsAbstract == false).ToArray();

                return types;
            }
            catch (Exception e)
            {
                GlobalConstants.ActionLog.StackTrace(e);
                GlobalConstants.ActionLog.Log("Error when searching for Type in ScriptingEngine, type name " + nameof(type), LogLevel.Error);
                return default;
            }
        }

        public T Evaluate<T>(string code)
        {
            return (T)Convert.ChangeType(this.Eval.Evaluate(code), typeof(T));
        }
    }
}