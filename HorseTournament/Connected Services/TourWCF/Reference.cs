﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HorseTournament.TourWCF {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Record", Namespace="http://schemas.datacontract.org/2004/07/")]
    [System.SerializableAttribute()]
    public partial class Record : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstPlayerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RoomCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SecondPlayerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TotalScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WinField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime dateTimeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstPlayer {
            get {
                return this.FirstPlayerField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstPlayerField, value) != true)) {
                    this.FirstPlayerField = value;
                    this.RaisePropertyChanged("FirstPlayer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RoomCode {
            get {
                return this.RoomCodeField;
            }
            set {
                if ((this.RoomCodeField.Equals(value) != true)) {
                    this.RoomCodeField = value;
                    this.RaisePropertyChanged("RoomCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SecondPlayer {
            get {
                return this.SecondPlayerField;
            }
            set {
                if ((object.ReferenceEquals(this.SecondPlayerField, value) != true)) {
                    this.SecondPlayerField = value;
                    this.RaisePropertyChanged("SecondPlayer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TotalScore {
            get {
                return this.TotalScoreField;
            }
            set {
                if ((this.TotalScoreField.Equals(value) != true)) {
                    this.TotalScoreField = value;
                    this.RaisePropertyChanged("TotalScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Win {
            get {
                return this.WinField;
            }
            set {
                if ((object.ReferenceEquals(this.WinField, value) != true)) {
                    this.WinField = value;
                    this.RaisePropertyChanged("Win");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime dateTime {
            get {
                return this.dateTimeField;
            }
            set {
                if ((this.dateTimeField.Equals(value) != true)) {
                    this.dateTimeField = value;
                    this.RaisePropertyChanged("dateTime");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TourWCF.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SetData", ReplyAction="http://tempuri.org/IService/SetDataResponse")]
        void SetData(HorseTournament.TourWCF.Record[] data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SetData", ReplyAction="http://tempuri.org/IService/SetDataResponse")]
        System.Threading.Tasks.Task SetDataAsync(HorseTournament.TourWCF.Record[] data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetData", ReplyAction="http://tempuri.org/IService/GetDataResponse")]
        HorseTournament.TourWCF.Record[] GetData();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetData", ReplyAction="http://tempuri.org/IService/GetDataResponse")]
        System.Threading.Tasks.Task<HorseTournament.TourWCF.Record[]> GetDataAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SetField", ReplyAction="http://tempuri.org/IService/SetFieldResponse")]
        void SetField(GameClientSpace.GameClient data, string room);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SetField", ReplyAction="http://tempuri.org/IService/SetFieldResponse")]
        System.Threading.Tasks.Task SetFieldAsync(GameClientSpace.GameClient data, string room);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetField", ReplyAction="http://tempuri.org/IService/GetFieldResponse")]
        GameClientSpace.GameClient GetField(string room);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetField", ReplyAction="http://tempuri.org/IService/GetFieldResponse")]
        System.Threading.Tasks.Task<GameClientSpace.GameClient> GetFieldAsync(string room);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ContainsRoom", ReplyAction="http://tempuri.org/IService/ContainsRoomResponse")]
        bool ContainsRoom(string room);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ContainsRoom", ReplyAction="http://tempuri.org/IService/ContainsRoomResponse")]
        System.Threading.Tasks.Task<bool> ContainsRoomAsync(string room);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : HorseTournament.TourWCF.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<HorseTournament.TourWCF.IService>, HorseTournament.TourWCF.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void SetData(HorseTournament.TourWCF.Record[] data) {
            base.Channel.SetData(data);
        }
        
        public System.Threading.Tasks.Task SetDataAsync(HorseTournament.TourWCF.Record[] data) {
            return base.Channel.SetDataAsync(data);
        }
        
        public HorseTournament.TourWCF.Record[] GetData() {
            return base.Channel.GetData();
        }
        
        public System.Threading.Tasks.Task<HorseTournament.TourWCF.Record[]> GetDataAsync() {
            return base.Channel.GetDataAsync();
        }
        
        public void SetField(GameClientSpace.GameClient data, string room) {
            base.Channel.SetField(data, room);
        }
        
        public System.Threading.Tasks.Task SetFieldAsync(GameClientSpace.GameClient data, string room) {
            return base.Channel.SetFieldAsync(data, room);
        }
        
        public GameClientSpace.GameClient GetField(string room) {
            return base.Channel.GetField(room);
        }
        
        public System.Threading.Tasks.Task<GameClientSpace.GameClient> GetFieldAsync(string room) {
            return base.Channel.GetFieldAsync(room);
        }
        
        public bool ContainsRoom(string room) {
            return base.Channel.ContainsRoom(room);
        }
        
        public System.Threading.Tasks.Task<bool> ContainsRoomAsync(string room) {
            return base.Channel.ContainsRoomAsync(room);
        }
    }
}
