﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Differences.Common.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Errors {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Errors() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Differences.Common.Resources.Errors", typeof(Errors).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        public static string AnswerContentLengthExceeding {
            get {
                return ResourceManager.GetString("AnswerContentLengthExceeding", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 该回复的内容太短了（应大于等于5个字）。.
        /// </summary>
        public static string AnswerContentLengthTooShort {
            get {
                return ResourceManager.GetString("AnswerContentLengthTooShort", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 该回答不存在。.
        /// </summary>
        public static string AnswerNotExists {
            get {
                return ResourceManager.GetString("AnswerNotExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 该问题的内容超过了最大长度：200。.
        /// </summary>
        public static string QuestionContentLengthExceeding {
            get {
                return ResourceManager.GetString("QuestionContentLengthExceeding", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 该问题的内容太短了（应大于等于5个字）。.
        /// </summary>
        public static string QuestionContentLengthTooShort {
            get {
                return ResourceManager.GetString("QuestionContentLengthTooShort", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 该问题不存在。.
        /// </summary>
        public static string QuestionNotExists {
            get {
                return ResourceManager.GetString("QuestionNotExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 该问题的标题超过了最大长度：60。.
        /// </summary>
        public static string QuestionTitleLengthExceeding {
            get {
                return ResourceManager.GetString("QuestionTitleLengthExceeding", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 该问题的标题太短了（应大于等于5个字）。.
        /// </summary>
        public static string QuestionTitleLengthTooShort {
            get {
                return ResourceManager.GetString("QuestionTitleLengthTooShort", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 用户没有权限。.
        /// </summary>
        public static string UserAccessDenied {
            get {
                return ResourceManager.GetString("UserAccessDenied", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 无效的授权，未能成功登陆。.
        /// </summary>
        public static string UserAuthCodeInvalid {
            get {
                return ResourceManager.GetString("UserAuthCodeInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 找不到用户。.
        /// </summary>
        public static string UserNotFound {
            get {
                return ResourceManager.GetString("UserNotFound", resourceCulture);
            }
        }
    }
}
