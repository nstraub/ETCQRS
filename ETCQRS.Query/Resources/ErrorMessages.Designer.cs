﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ETCQRS.Query.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ETCQRS.Query.Resources.ErrorMessages", typeof(ErrorMessages).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You are already building an expression. Finalize it by invoking the appropriate AddExpression method before creating a new one.
        /// </summary>
        internal static string AlreadyBuildingQuery {
            get {
                return ResourceManager.GetString("AlreadyBuildingQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Only conditional logic operators, &quot;AndAlso&quot; and &quot;OrElse&quot; are allowed.
        /// </summary>
        internal static string InvalidQueryLinkerExpression {
            get {
                return ResourceManager.GetString("InvalidQueryLinkerExpression", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is no mutator to set next.
        /// </summary>
        internal static string NextMutatorNullReference {
            get {
                return ResourceManager.GetString("NextMutatorNullReference", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You must provide a property to compare the value to.
        /// </summary>
        internal static string PropertyNullReference {
            get {
                return ResourceManager.GetString("PropertyNullReference", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is no query to mutate.
        /// </summary>
        internal static string QueryNullReference {
            get {
                return ResourceManager.GetString("QueryNullReference", resourceCulture);
            }
        }
    }
}
