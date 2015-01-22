using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace ServiceStack.AzureSQL
{
    [XmlRoot("ServiceResource")]
    public class DatabaseRestore : ServiceResourceBase
    {
        /// <summary>The identifier for this request.</summary>
        [XmlElement("RequestID")]
        public Guid RequestID { get; set; }

        /// <summary>The name of the database to restore.</summary>
        [XmlElement("SourceDatabaseName")]
        public string SourceDatabaseName { get; set; }

        /// <summary>The date and time when the database was deleted.</summary>
        [XmlElement("SourceDatabaseDeletionDate")]
        public DateTime SourceDatabaseDeletionDate { get; set; }

        /// <summary>The name of the new database after restore.</summary>
        [XmlElement("TargetDatabaseName")]
        public string TargetDatabaseName { get; set; }

        /// <summary>The point in time the database was restored from.</summary>
        [XmlElement("TargetUtcPointInTime")]
        public DateTime TargetUtcPointInTime { get; set; }
    }

    [XmlRoot("ServiceResource")]
    public class FirewallRule : ServiceResourceBase
    {
        /// <summary>The lowest IP address in the range of the firewall rule.</summary>
        [XmlElement("StartIPAddress")]
        public string StartIPAddress { get; set; }

        /// <summary>The highest IP address in the range of the firewall rule.</summary>
        [XmlElement("EndIPAddress")]
        public string EndIPAddress { get; set; }
    }

    [XmlRoot("ServiceResources")]
    public class FirewallRules : List<FirewallRule> { }

    [XmlRoot("ServerName")]
    public class ServerName
    {
        /// <summary>The fully qualified domain name of the newly created server.</summary>
        [XmlAttribute("FullyQualifiedDomainName")]
        public string FullyQualifiedDomainName { get; set; }

        /// <summary>The name of the newly created server.</summary>
        [XmlText()]
        public string Name { get; set; }
    }

    [XmlRoot("ServiceResources")]
    public class GenericServers : List<GenericServer> { }

    [XmlRoot("ServiceResource")]
    public class GenericServer : ServiceResourceBase
    {
        /// <summary>The fully qualified domain name of the server.</summary>
        [XmlAttribute("FullyQualifiedDomainName")]
        public string FullyQualifiedDomainName { get; set; }
    }

    [XmlRoot("Servers")]
    public class Servers : List<Server> { }

    [XmlRoot("Server")]
    public class Server
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("AdministratorLogin")]
        public string AdministratorLogin { get; set; }

        [XmlElement("Location")]
        public string Location { get; set; }

        [XmlElement("GeoPairedRegion")]
        public string GeoPairedRegion { get; set; }

        /// <summary>The fully qualified domain name of the server.</summary>
        [XmlElement("FullyQualifiedDomainName")]
        public string FullyQualifiedDomainName { get; set; }

        [XmlElement("Version")]
        public string Version { get; set; }
    }


    [XmlRoot("ServiceResource")]
    public class DatabaseOperationStatusDetail : ServiceResourceBase
    {
        /// <summary>
        /// The identifier for this resource.
        /// </summary>
        [XmlElement("Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// The identifier for the state of this resource.
        /// </summary>
        [XmlElement("StateId")]
        public int StateId { get; set; }

        /// <summary>
        /// The identifier for the session activity.
        /// </summary>
        [XmlElement("SessionActivityId")]
        public Guid SessionActivityId { get; set; }

        /// <summary>
        /// The name of the database.
        /// </summary>
        [XmlElement("DatabaseName")]
        public string DatabaseName { get; set; }

        /// <summary>
        /// The percent complete for this operation.
        /// </summary>
        [XmlElement("PercentComplete")]
        public int PercentComplete { get; set; }

        /// <summary>
        /// Specifies an error code if an error occurs.
        /// </summary>
        [XmlElement("ErrorCode")]
        public int? ErrorCode { get; set; }

        /// <summary>
        /// Specifies a description of the error if an error occurs.
        /// </summary>
        [XmlElement("Error")]
        public string Error { get; set; }

        /// <summary>
        /// Specifies the error severity level if an error occurs.
        /// </summary>
        [XmlElement("ErrorSeverity")]
        public int? ErrorSeverity { get; set; }

        /// <summary>
        /// Specifies the state of the error if an error occurs.
        /// </summary>
        [XmlElement("ErrorState")]
        public int? ErrorState { get; set; }

        /// <summary>
        /// Specifies the start time of the operation.
        /// </summary>
        [XmlElement("StartTime")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Specifies the last modify time of the operation.
        /// </summary>
        [XmlElement("LastModifyTime")]
        public DateTime? LastModifyTime { get; set; }
    }

    [XmlRoot("ServiceResources")]
    public class DatabaseOperationStatusDetails : List<DatabaseOperationStatusDetail> { }

    [XmlRoot("ServiceResource")]
    public class Database : ServiceResourceBase
    {
        /// <summary>
        /// The database ID. Each database in a server has a unique ID.
        /// </summary>
        [XmlElement("Id")]
        public int Id { get; set; }

        /// <summary>
        /// The current edition of the database. If the edition was changed 
        /// during an update, this will be the old value until any pending 
        /// Service Level Objective (SLO) assignments are completed. For 
        /// edition changes that don’t require a SLO change, this will be 
        /// the new edition.
        /// </summary>
        [XmlElement("Edition")]
        public string Edition { get; set; }

        /// <summary>
        /// The maximum size of the database in gigabytes.
        /// </summary>
        [XmlElement("MaxSizeGB")]
        public int MaxSizeGB { get; set; }

        /// <summary>
        /// The name of the database collation.
        /// </summary>
        [XmlElement("CollationName")]
        public string CollationName { get; set; }

        /// <summary>
        /// The date and time this database was created.
        /// </summary>
        [XmlElement("CreationDate")]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Specifies if this database is the federation root.
        /// </summary>
        [XmlElement("IsFederationRoot")]
        public bool IsFederationRoot { get; set; }

        /// <summary>
        /// Specifies if this database is a system object. 
        /// Master database is an example of a system object.
        /// </summary>
        [XmlElement("IsSystemObject")]
        public bool IsSystemObject { get; set; }

        /// <summary>
        /// The currently used size of the database in megabytes.
        /// </summary>
        [XmlElement("SizeMB", IsNullable = true)]
        public int? SizeMB { get; set; }

        /// <summary>
        /// The maximum size of the database expressed in bytes.
        /// </summary>
        [XmlElement("MaxSizeBytes")]
        public int MaxSizeBytes { get; set; }

        /// <summary>
        /// The currently assigned and active service objective ID. If a SLO 
        /// change is in progress or pending, this will be the SLO before the 
        /// update was applied.
        /// 
        /// To know which GUID to use in ServiceObjectiveId, see 
        /// <see cref="http://msdn.microsoft.com/en-us/library/azure/dn505723.aspx"> 
        /// List Service Level Objectives</see> find the ID of the service 
        /// that you want, and locate the GUID for that service.
        /// </summary>
        [XmlElement("ServiceObjectiveId")]
        public Guid ServiceObjectiveId { get; set; }

        /// <summary>
        /// The currently assigned service objective ID. If a SLO change is 
        /// in progress or pending this will be the new SLO that is assigned 
        /// to the database
        /// </summary>
        [XmlElement("AssignerServiceObjectiveId")]
        public Guid AssignedServiceObjectiveId { get; set; }

        /// <summary>
        /// An integer representing the current state of the service objective 
        /// assignment operation. 1 is complete and 0 is pending.
        /// </summary>
        [XmlElement("ServiceObjectiveAssignmentState")]
        public int ServiceObjectiveAssignmentState { get; set; }

        /// <summary>
        /// The current state of the service objective assignment. Pending 
        /// means the database is currently transitioning from one Service 
        /// Objective to another. Complete means that the service objective 
        /// assignment completed successfully.
        /// </summary>
        [XmlElement("ServiceObjectiveAssignementStateDescription")]
        public string ServiceObjectiveAssignementStateDescription { get; set; }

        /// <summary>
        /// If there was an error assigning the service objective to the 
        /// database, this will contain the error code.
        /// </summary>
        [XmlElement("ServiceObjectiveAssignmentErrorCode")]
        public string ServiceObjectiveAssignmentErrorCode { get; set; }

        /// <summary>
        /// The description for the error, if there was an error.
        /// </summary>
        [XmlElement("ServiceObjectiveAssignmentErrorDescription")]
        public string ServiceObjectiveAssignmentErrorDescription { get; set; }

        /// <summary>
        /// The date and time that the service objective was successfully 
        /// applied to the database.
        /// </summary>
        [XmlElement("ServiceObjectiveAssignmentSuccessDate")]
        public DateTime ServiceObjectiveAssignmentSuccessDate { get; set; }

        /// <summary>
        /// The starting date for when database recovery is available.
        /// </summary>
        [XmlElement("RecoveryPeriodStartDate")]
        public DateTime? RecoveryPeriodStartDate { get; set; }
    }


    [XmlRoot("ServiceResource")]
    public class DatabaseCopy : ServiceResourceBase
    {
        /// <summary>
        /// The server that is the source for the copy.
        /// </summary>
        [XmlElement("SourceServerName")]
        public string SourceServerName { get; set; }

        /// <summary>
        /// The database that is the source for the copy.
        /// </summary>
        [XmlElement("SourceDatabaseName")]
        public string SourceDatabaseName { get; set; }

        /// <summary>
        /// The server that is the destination for the copy.
        /// </summary>
        [XmlElement("DestinationServerName")]
        public string DestinationServerName { get; set; }

        /// <summary>
        /// The database that is the destination for the copy.
        /// </summary>
        [XmlElement("DestinationDatabaseName")]
        public string DestinationDatabaseName { get; set; }

        /// <summary>
        /// Specifies that the copy is a continuous copy.
        /// </summary>
        [XmlElement("IsContinuous")]
        public bool IsContinuous { get; set; }

        /// <summary>
        /// When <b>True</b>, specifies that the copy should be a passive 
        /// continuous copy.
        /// </summary>
        [XmlElement("IsOfflineSecondary")]
        public bool IsOfflineSecondary { get; set; }

        /// <summary>
        /// When <b>True</b>, specifies that the passive continuous copy has 
        /// permission to failover.
        /// </summary>
        [XmlElement("IsTerminationAllowed")]
        public bool IsTerminationAllowed { get; set; }

        /// <summary>
        /// The time the copy was started.
        /// </summary>
        [XmlElement("StartDate")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The time the copy was last modified.
        /// </summary>
        [XmlElement("ModifyDate")]
        public DateTime ModifyDate { get; set; }

        /// <summary>
        /// Percent complete of the copy. For continuous copies, this only 
        /// applies to the SEEDING (initial copy) phase.
        /// </summary>
        [XmlElement("PercentComplete")]
        public int PercentComplete { get; set; }

        /// <summary>
        /// An integer representing the replication state of the database. 
        /// Possible values are:
        /// 0: PENDING (the copy hasn't started yet)
        /// 
        /// 1: SEEDING (the initial copy is in progress)
        /// 
        /// 2: CATCH_UP (for continuous copies, indicates that the initial copy 
        /// is complete and asynchronous commits are being propagated to the 
        /// target)
        /// 
        /// 4: TERMINATED
        /// 
        /// Other values (like 3) are possible. The corresponding 
        /// ReplicationStateDescription would be NULL).
        /// The meaning of these values will not change in future releases.
        /// </summary>
        [XmlElement("ReplicationState")]
        public int ReplicationState { get; set; }

        /// <summary>
        /// The string description for the ReplicationState (see ReplicationState).
        /// </summary>
        [XmlElement("ReplicationStateDescription")]
        public string ReplicationStateDescription { get; set; }

        /// <summary>
        /// The ID of the local database (the same as the id in sys.databases). 
        /// If the resource path for the request is to the source database, 
        /// this will be the ID of the source databases. If the resource path 
        /// for the request is to the target database, this will be the ID of 
        /// the target database.
        /// </summary>
        [XmlElement("LocationDatabaseId")]
        public int LocationDatabaseId { get; set; }

        /// <summary>
        /// Whether or not the local database (see LocalDatabaseId) is the target for the copy.
        /// </summary>
        [XmlElement("IsLocalDatabaseReplicationTarget")]
        public bool IsLocalDatabaseReplicationTarget { get; set; }

        /// <summary>
        /// If the database copy is interlink connected.
        /// </summary>
        [XmlElement("IsInterlinkConnected")]
        public bool IsInterlinkConnected { get; set; }
    }


    [XmlRoot("ServiceResource")]
    public class EventLog : ServiceResourceBase
    {
        /// <summary>
        /// The name of the database this log is from.
        /// </summary>
        [XmlElement("DatabaseName")]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Starting time of the event log.
        /// </summary>
        [XmlElement("StartTimeUtc")]
        public DateTime StartTimeUtc { get; set; }

        /// <summary>
        /// The number of minutes of aggregate log entries.
        /// </summary>
        [XmlElement("IntervalSizeInMinutes")]
        public int IntervalSizeInMinutes { get; set; }

        /// <summary>
        /// The event category of the log entries.
        /// </summary>
        [XmlElement("EventCategory")]
        public string EventCategory { get; set; }

        /// <summary>
        /// The event type of the log entries.
        /// </summary>
        [XmlElement("EventType")]
        public string EventType { get; set; }

        /// <summary>
        /// The event subtype of the log entries.
        /// </summary>
        [XmlElement("EventSubtype", IsNullable = true)]
        public int? EventSubtype { get; set; }

        /// <summary>
        /// The description of the event subtype.
        /// </summary>
        [XmlElement("EventSubtypeDescription")]
        public string EventSubtypeDescription { get; set; }

        /// <summary>
        /// Number of aggregate events.
        /// </summary>
        [XmlElement("NumberOfEvents")]
        public int NumberOfEvents { get; set; }

        /// <summary>
        /// Specifies the severity of the event subtype.
        /// </summary>
        [XmlElement("Severity")]
        public int Severity { get; set; }

        /// <summary>
        /// Specifies the description of the event.
        /// </summary>
        [XmlElement("Description")]
        public string Description { get; set; }

        /// <summary>
        /// Additional data
        /// </summary>
        [XmlElement("AdditionalData", IsNullable = true)]
        public string AdditionalData { get; set; }
    }

    [XmlRoot("ServiceResources")]
    public class EventLogs : List<EventLogs> { }


    [XmlRoot("ServiceResource")]
    public class Quota : ServiceResourceBase
    {
        /// <summary>
        /// The value associated with the quota. For example, a quota name of 
        /// premium_databases, and a value of 2 indicates that the server can 
        /// have at most 2 premium databases.
        /// </summary>
        [XmlElement("Value")]
        public int Value { get; set; }
    }

    [XmlRoot("ServiceResources")]
    public class Quotas : List<Quota> { }

    [XmlRoot("ServiceResource")]
    public class SubscriptionMetadata : ServiceResourceBase
    {
        public SubscriptionMetadata()
        {
            Versions = new List<Version>();
        }

        [XmlElement("DatabaseQuota")]
        public int DatabaseQuota { get; set; }

        [XmlElement("Id")]
        public Guid Id { get; set; }

        [XmlElement("ServerQuota")]
        public int ServerQuota { get; set; }

        [XmlElement("HasFreeDatabase")]
        public bool HasFreeDatabase { get; set; }

        [XmlElement("Locations", IsNullable = true)]
        public string Locations { get; set; }

        [XmlArray("Versions")]
        [XmlArrayItem("Version", Type = typeof(Version))]
        public List<Version> Versions { get; set; }
    }

    public class Version
    {
        public Version()
        {
            Editions = new List<Edition>();
        }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("IsDefault")]
        public bool IsDefault { get; set; }

        [XmlArray("Editions")]
        [XmlArrayItem("Edition", Type = typeof(Edition))]
        public List<Edition> Editions { get; set; }
    }

    public class Edition
    {
        public Edition()
        {
            ServiceLevelObjectives = new List<ServiceLevelObjective>();
        }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("IsDefault")]
        public bool IsDefault { get; set; }

        [XmlArray("ServiceLevelObjectives")]
        [XmlArrayItem("ServiceLevelObjective", Type = typeof(ServiceLevelObjective))]
        public List<ServiceLevelObjective> ServiceLevelObjectives { get; set; }


    }


    [XmlRoot("ServiceResources")]
    public class ServiceLevelObjectives : List<ServiceObjective> { }

    [XmlRoot("ServiceResource")]
    public class ServiceLevelObjective
    {
        public ServiceLevelObjective()
        {
            MaxSizes = new List<MaxSize>();
        }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("IsDefault")]
        public bool IsDefault { get; set; }

        [XmlElement("ID")]
        public Guid ID { get; set; }

        [XmlArray("MaxSizes")]
        [XmlArrayItem("MaxSize", typeof(MaxSize))]
        public List<MaxSize> MaxSizes { get; set; }

        [XmlElement("PerformanceLevel")]
        public PerformanceLevel PerformanceLevel { get; set; }
    }


    public class PerformanceLevel
    {
        [XmlElement("Value")]
        public int Value { get; set; }

        [XmlElement("Unit")]
        public string Unit { get; set; }
    }

    public class MaxSize
    {
        [XmlElement("Value")]
        public int Value { get; set; }

        [XmlElement("Unit")]
        public string Unit { get; set; }

        [XmlElement("IsDefault")]
        public bool IsDefault { get; set; }
    }

    [XmlRoot("ServiceResource")]
    public class ServiceObjective : ServiceResourceBase
    {
        public ServiceObjective()
        {
            DimensionSettings = new List<DimensionSetting>();
        }

        [XmlElement("Id")]
        public Guid Id { get; set; }

        [XmlElement("IsDefault")]
        public bool IsDefault { get; set; }

        [XmlElement("IsSystem")]
        public bool IsSystem { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("Enabled")]
        public bool Enabled { get; set; }

        [XmlArray("DimensionSettings")]
        [XmlArrayItem("ServiceResource", Type = typeof(DimensionSetting))]
        public List<DimensionSetting> DimensionSettings { get; set; }
    }

    [XmlRoot("ServiceResource")]
    public class DimensionSetting : ServiceResourceBase
    {
        [XmlElement("Id")]
        public Guid Id { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("Ordinal")]
        public int Ordinal { get; set; }

        [XmlElement("IsDefault")]
        public bool IsDefault { get; set; }
    }

    [XmlRoot("ServiceResource")]
    public class RecoverableDatabase : ServiceResourceBase
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Edition")]
        public string Edition { get; set; }

        [XmlElement("MaxSizeGB")]
        public int MaxSizeGB { get; set; }

        [XmlElement("CreationDate")]
        public DateTime CreationDate { get; set; }

        [XmlElement("LastAvailableBackupDate")]
        public DateTime LastAvailableBackupDate { get; set; }
    }


    [XmlRoot("StatusInfo")]
    public class StatusInfo
    {
        [XmlElement("BlobUri")]
        public string BlobUri { get; set; }

        [XmlElement("DatabaseName")]
        public string DatabaseName { get; set; }

        [XmlElement("ErrorMessage", IsNullable = true)]
        public string ErrorMessage { get; set; }

        [XmlElement("LastModifiedTime")]
        public DateTime LastModifiedTime { get; set; }

        [XmlElement("QueuedTime")]
        public DateTime QueuedTime { get; set; }

        [XmlElement("RequestId")]
        public Guid RequestId { get; set; }

        [XmlElement("RequestType")]
        public string RequestType { get; set; }

        [XmlElement("ServerName")]
        public string ServerName
        {
            get
            {
                return serverName;
            }
            set
            {
                serverName = value;
                if (!serverName.EndsWithIgnoreCase(".database.windows.net"))
                    serverName += ".database.windows.net";
            }
        }
        private string serverName;

        [XmlElement("Status")]
        public string Status { get; set; }
    }

    [XmlRoot("ArrayOfStatusInfo", Namespace="http://schemas.datacontract.org/2004/07/Microsoft.SqlServer.Management.Dac.ServiceTypes")]
    public class StatusInfos : List<StatusInfo>
    { }

#region Base Classes

    public abstract class ServiceResourceBase
    {
        /// <summary>The name of the new firewall rule.</summary>
        [XmlElement("Name")]
        public virtual string Name { get; set; }

        /// <summary>The type of the service resource; Microsoft.SqlAzure.FirewallRule.</summary>
        [XmlElement("Type")]
        public virtual string Type { get; set; }

        /// <summary>The state of the service resource.</summary>
        [XmlElement("State", IsNullable = true)]
        public virtual string State { get; set; }

        /// <summary>The URI identifier for this resource.</summary>
        [XmlElement("SelfLink", IsNullable = true)]
        public virtual string SelfLink { get; set; }

        /// <summary>The URI identifier for the parent of this resource (the server).</summary>
        [XmlElement("ParentLink", IsNullable = true)]
        public virtual string ParentLink { get; set; }
    }


#endregion
}
