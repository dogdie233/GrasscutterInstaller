﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace GrasscutterInstaller.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ProgramText {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ProgramText() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GrasscutterInstaller.Properties.ProgramText", typeof(ProgramText).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性，对
        ///   使用此强类型资源类的所有资源查找执行重写。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 Grasscutter run! 的本地化字符串。
        /// </summary>
        internal static string ApplicationRun {
            get {
                return ResourceManager.GetString("ApplicationRun", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Can&apos;t found branch &quot;{0}&quot; 的本地化字符串。
        /// </summary>
        internal static string BranchInputNotFound {
            get {
                return ResourceManager.GetString("BranchInputNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Branch input is null 的本地化字符串。
        /// </summary>
        internal static string BranchInputNull {
            get {
                return ResourceManager.GetString("BranchInputNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Download {0} 的本地化字符串。
        /// </summary>
        internal static string DownloadTaskName {
            get {
                return ResourceManager.GetString("DownloadTaskName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Download {0} zip failed, please retry. 的本地化字符串。
        /// </summary>
        internal static string DownloadZipFailed {
            get {
                return ResourceManager.GetString("DownloadZipFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Grasscutter License: {0} 的本地化字符串。
        /// </summary>
        internal static string GCLicenseKey {
            get {
                return ResourceManager.GetString("GCLicenseKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Getting {0} branches... 的本地化字符串。
        /// </summary>
        internal static string GetBranches {
            get {
                return ResourceManager.GetString("GetBranches", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Getting {0} repository details... 的本地化字符串。
        /// </summary>
        internal static string GetRepository {
            get {
                return ResourceManager.GetString("GetRepository", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Task {0} finished, used time: {1}ms 的本地化字符串。
        /// </summary>
        internal static string ProgressBarTaskFinishFormat {
            get {
                return ResourceManager.GetString("ProgressBarTaskFinishFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Start task {0} 的本地化字符串。
        /// </summary>
        internal static string ProgressBarTaskStartFormat {
            get {
                return ResourceManager.GetString("ProgressBarTaskStartFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Please select a branch in ({0}): 的本地化字符串。
        /// </summary>
        internal static string SelectBranch {
            get {
                return ResourceManager.GetString("SelectBranch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Extract {0} zip to Directory {1} 的本地化字符串。
        /// </summary>
        internal static string UnzipTaskName {
            get {
                return ResourceManager.GetString("UnzipTaskName", resourceCulture);
            }
        }
    }
}