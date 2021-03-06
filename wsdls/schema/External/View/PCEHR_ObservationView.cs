﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.8009
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=2.0.50727.3038.
// 
namespace Nehta.VendorLibrary.PCEHR.ObservationView {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://ns.electronichealth.net.au/pcehr/xsd/interfaces/ObservationView/1.0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://ns.electronichealth.net.au/pcehr/xsd/interfaces/ObservationView/1.0", IsNullable=false)]
    public partial class observationView {
        
        private string versionNumberField;
        
        private System.DateTime fromDateField;
        
        private System.DateTime toDateField;
        
        private observationViewObservationType observationTypeField;
        
        private observationViewDocumentSource documentSourceField;
        
        private observationViewReferenceData referenceDataField;
        
        /// <remarks/>
        public string versionNumber {
            get {
                return this.versionNumberField;
            }
            set {
                this.versionNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
        public System.DateTime fromDate {
            get {
                return this.fromDateField;
            }
            set {
                this.fromDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
        public System.DateTime toDate {
            get {
                return this.toDateField;
            }
            set {
                this.toDateField = value;
            }
        }
        
        /// <remarks/>
        public observationViewObservationType observationType {
            get {
                return this.observationTypeField;
            }
            set {
                this.observationTypeField = value;
            }
        }
        
        /// <remarks/>
        public observationViewDocumentSource documentSource {
            get {
                return this.documentSourceField;
            }
            set {
                this.documentSourceField = value;
            }
        }
        
        /// <remarks/>
        public observationViewReferenceData referenceData {
            get {
                return this.referenceDataField;
            }
            set {
                this.referenceDataField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://ns.electronichealth.net.au/pcehr/xsd/interfaces/ObservationView/1.0")]
    public enum observationViewObservationType {
        
        /// <remarks/>
        HEADCIRCUMFERENCE,
        
        /// <remarks/>
        HEIGHT,
        
        /// <remarks/>
        WEIGHT,
        
        /// <remarks/>
        BMI,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://ns.electronichealth.net.au/pcehr/xsd/interfaces/ObservationView/1.0")]
    public enum observationViewDocumentSource {
        
        /// <remarks/>
        PERSONAL,
        
        /// <remarks/>
        PROVIDER,
        
        /// <remarks/>
        ALL,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://ns.electronichealth.net.au/pcehr/xsd/interfaces/ObservationView/1.0")]
    public enum observationViewReferenceData {
        
        /// <remarks/>
        CDC,
        
        /// <remarks/>
        WHO,
    }
}
