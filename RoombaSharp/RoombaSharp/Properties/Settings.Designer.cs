﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RoombaSharp.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("iot.eclipse.org")]
        public string BrokerHost {
            get {
                return ((string)(this["BrokerHost"]));
            }
            set {
                this["BrokerHost"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1883")]
        public int BrokerPort {
            get {
                return ((int)(this["BrokerPort"]));
            }
            set {
                this["BrokerPort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("pt/str/i/robot/2")]
        public string MqttInputTopic {
            get {
                return ((string)(this["MqttInputTopic"]));
            }
            set {
                this["MqttInputTopic"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("pt/str/o/robot/2")]
        public string MqttOutputTopic {
            get {
                return ((string)(this["MqttOutputTopic"]));
            }
            set {
                this["MqttOutputTopic"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("pt/img/o/robot/2")]
        public string MqttImageTopic {
            get {
                return ((string)(this["MqttImageTopic"]));
            }
            set {
                this["MqttImageTopic"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("300, 300")]
        public global::System.Drawing.Size ImageSize {
            get {
                return ((global::System.Drawing.Size)(this["ImageSize"]));
            }
            set {
                this["ImageSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1000")]
        public int UpdateInterval {
            get {
                return ((int)(this["UpdateInterval"]));
            }
            set {
                this["UpdateInterval"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\\\Users\\\\POLYGONTeam Ltd\\\\Documents\\\\GitHub\\\\iRobot\\\\RoombaSharp\\\\RoombaSharp\\\\" +
            "bin\\\\Debug\\\\Settings\\\\ScheduleData.XML")]
        public string SchedulingSettings {
            get {
                return ((string)(this["SchedulingSettings"]));
            }
            set {
                this["SchedulingSettings"] = value;
            }
        }
    }
}
