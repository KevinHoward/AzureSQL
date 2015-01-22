using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using ServiceStack;
using ServiceStack.DataAnnotations;

using ServiceStack.AzureSQL.Types;

namespace ServiceStack.AzureSQL
{
    #region Subscription Requests

    [Route("/{SubscriptionId}/services/sqlservers/subscriptioninfo",
    Verbs = "GET", Summary = "Retrieves the metadata for a Microsoft Azure subscription.")]
    public class GetSubscriptionMetadata : IGet, IReturn<SubscriptionMetadata>
    { 
        // Path

        public string SubscriptionId { get; set; }
    }

    #endregion

    #region Server Requests

    /// <summary>
    /// Create a new Azure SQL Database server.
    /// </summary>
    /// <see cref="http://msdn.microsoft.com/en-us/library/azure/dn505699.aspx"/>
    [XmlRoot("Server")]
    [Route("/{SubscriptionId}/services/sqlservers/servers",
        Verbs = "POST", Summary = "Create a new Azure SQL Database server.")]
    public class CreateServer : IPost, IReturn<ServerName>
    {
        // Path

        /// <summary>The subscription ID.</summary>
        [XmlIgnore()]
        public string SubscriptionId { get; set; }

        // Body

        /// <summary>The administrator login name for the new server.</summary>
        [XmlElement("AdministratorLogin", IsNullable = false)]
        public string AdministratorLogin { get; set; }

        /// <summary>The administrator login password for the new server.</summary>
        [XmlElement("AdministratorPassword", IsNullable = false)]
        public string AdministratorPassword { get; set; }

        /// <summary>The region to deploy the new server.</summary>
        [XmlElement("Location", IsNullable = false, Type = typeof(DataCenter))]
        public DataCenter Location { get; set; }
    }

    /// <summary>
    /// Move an Azure SQL Database server to a different subscription.
    /// </summary>
    /// <see cref="http://msdn.microsoft.com/en-us/library/azure/dn479912.aspx"/>
    [XmlRoot("TargetSubscriptionId")]
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}?op=ChangeSubscription",
        Verbs = "POST", Summary = "Move an Azure SQL Database server to a different subscription.")]
    public class ChangeSubscriptionForSingleServer : IPost, IReturn
    {
        // Path

        /// <summary>The source subscription ID (the target subscription ID is specified in the request body).</summary>
        [XmlIgnore()]
        public string SubscriptionId { get; set; }

        /// <summary>The name of the server you want to move to a different subscription.</summary>
        [XmlIgnore()]
        public string ServerName { get; set; }

        // Body

        /// <summary>The subscription ID to move the server to.</summary>
        [XmlText()]
        public string TargetSubscriptionId { get; set; }
    }

    /// <summary>
    /// Move all Azure SQL Database servers from one subscription to a 
    /// different subscription.
    /// </summary>
    /// <see cref="http://msdn.microsoft.com/en-us/library/azure/dn505714.aspx"/>
    [XmlRoot("TargetSubscriptionId")]
    [Route("/{SubscriptionId}/services/sqlservers/servers?op=ChangeSubscription",
        Verbs = "POST", Summary = "Move all Azure SQL Database servers from one subscription to a different subscription.")]
    public class ChangeSubscriptionForAllServers : IPost, IReturn
    {
        // Path

        /// <summary>The source subscription ID (the target subscription ID is specified in the request body).</summary>
        [XmlIgnore()]
        public string SubscriptionId { get; set; }

        // Body

        /// <summary>The subscription ID to move the servers to.</summary>
        [XmlText()]
        public string TargetSubscriptionId { get; set; }
    }

    /// <summary>
    /// Deletes an Azure SQL Database server (including all its databases).
    /// </summary>
    /// <see cref="https://msdn.microsoft.com/en-us/library/azure/dn505695.aspx" />
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}",
        Verbs = "DELETE", Summary = "Deletes an Azure SQL Database server (including all its databases).")]
    public class DeleteServer : IDelete, IReturn
    {
        // Path

        /// <summary>
        /// The subscription ID.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// The name of the server you want to delete.
        /// </summary>
        public string ServerName { get; set; }
    }

    /// <summary>
    /// Retrieves the Azure SQL Database servers for a given subscription.
    /// </summary>
    /// <see cref="https://msdn.microsoft.com/en-us/library/azure/dn505702.aspx" />
    [Route("/{SubscriptionId}/services/sqlservers/servers?contentview=generic",
    Verbs = "GET", Summary = "Retrieves the Azure SQL Database servers for a given subscription.")]
    public class ListGenericServers : IGet, IReturn<GenericServers>
    {
        // Path

        /// <summary>
        /// The subscription ID.
        /// </summary>
        public string SubscriptionId { get; set; }
    }

    /// <summary>
    /// Retrieves the Azure SQL Database servers for a given subscription.
    /// </summary>
    /// <see cref="https://msdn.microsoft.com/en-us/library/azure/dn505702.aspx" />
    [Route("/{SubscriptionId}/services/sqlservers/servers",
    Verbs = "GET", Summary = "Retrieves the Azure SQL Database servers for a given subscription.")]
    public class ListServers : IGet, IReturn<Servers>
    {
        // Path

        /// <summary>
        /// The subscription ID.
        /// </summary>
        public string SubscriptionId { get; set; }
    }

    #endregion

    #region Database Requests

    /// <summary>
    /// Creates a new Microsoft Azure SQL Database.
    /// </summary>
    /// <see cref="http://msdn.microsoft.com/en-us/library/azure/dn505701.aspx"/>
    [XmlRoot("ServiceResource")]
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/databases",
        Verbs = "POST", Summary = "Creates a new Microsoft Azure SQL Database.")]
    public class CreateDatabase : IPost, IReturn<Database>
    {
        // Path

        /// <summary>The subscription ID.</summary>
        [XmlIgnore()]
        public string SubscriptionId { get; set; }

        /// <summary>The name of the server to contain the new database.</summary>
        [XmlIgnore()]
        public string ServerName { get; set; }


        // Body

        /// <summary>Required. The name for the new database. See <see cref="http://msdn.microsoft.com/library/azure/ee336245.aspx">Naming Requirements</see> in Azure SQL Database General Guidelines and Limitations and <see cref="http://msdn.microsoft.com/en-us/library/ms175874.aspx">Database Identifiers</see> for more information.</summary>
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        /// <summary>Optional. The Service Tier (Edition) for the new database. If omitted, the default is Web. Valid values are Web, Business, Basic, Standard, and Premium. See <see cref="http://msdn.microsoft.com/en-us/library/azure/dn741340.aspx">Azure SQL Database Service Tiers (Editions)</see> and <see cref="http://msdn.microsoft.com/en-us/library/azure/dn741330.aspx">Web and Business Edition Sunset FAQ</see> for more information.</summary>
        [XmlElement(ElementName = "Edition", IsNullable = true)]
        public string Edition { get; set; }

        /// <summary>Optional. The database collation. This can be any collation supported by SQL. If omitted, the default collation is used. See <see cref="http://msdn.microsoft.com/library/azure/ee336245.aspx">SQL Server Collation Support</see> in Azure SQL Database General Guidelines and Limitations for more information.</summary>
        [XmlElement(ElementName = "CollationName", IsNullable = true)]
        public string CollationName { get; set; }

        /// <summary>Optional. Sets the maximum size, in bytes, for the database. This value must be within the range of allowed values for <b>Edition</b>. If omitted, the default value for the edition is used. See <see cref="http://msdn.microsoft.com/en-us/library/azure/dn741340.aspx">Azure SQL Database Service Tiers (Editions)</see> for current maximum databases sizes. Convert MB or GB values to bytes. 1 MB = 1048576 bytes. 1 GB = 1073741824 bytes.</summary>
        [XmlElement(ElementName = "MaxSizeBytes", IsNullable = true)]
        public int? MaxSizeBytes { get; set; }

        /// <summary>Optional. The GUID corresponding to the performance level for <b>Edition</b>. See <see cref="http://msdn.microsoft.com/en-us/library/azure/dn505723.aspx">List Service Level Objectives</see> for current values.</summary>
        [XmlElement(ElementName = "ServiceObjectiveId", IsNullable = true)]
        public Guid? ServiceObjectiveId { get; set; }
    }

    /// <summary>
    /// Recover an Azure SQL Database.
    /// </summary>
    /// <see cref="http://msdn.microsoft.com/en-us/library/azure/dn800986.aspx"/>
    [XmlRoot("ServiceResource")]
    [Route("/{SubscriptionId}/servers/{SourceServerName}/recoverdatabaseoperations",
        Verbs = "POST", Summary = "Recover an Azure SQL Database.")]
    public class CreateDatabaseRecoveryRequest
    {
        private readonly IList<string> reservedDatabaseNames = new List<string> { "master", "tempdb", "model", "msdb" };

        // Path

        /// <summary>The subscription ID.</summary>
        [XmlIgnore()]
        public string SubscriptionId { get; set; }

        // Body

        /// <summary>The name of the database to recover.</summary>
        [XmlElement("SourceDatabaseName")]
        public string SourceDatabaseName
        {
            get { return sourceDatabaseName; }
            set
            {
                if (reservedDatabaseNames.Contains(value))
                {
                    throw new Exception("Provided Source Database Name {0} is invalid. You can not use a reserved database name ({1}).".Fmt(value, reservedDatabaseNames.Join(",")));
                }

                sourceDatabaseName = value;
            }
        }
        private string sourceDatabaseName;

        /// <summary>The name of the server to locate the recovered database.</summary>
        [XmlElement("SourceServerName")]
        public string TargetServerName
        {
            get { return targetDatabaseName; }
            set
            {
                if (reservedDatabaseNames.Contains(value))
                {
                    throw new Exception("Provided Target Database Name {0} is invalid. You can not use a reserved database name ({1}).".Fmt(value, reservedDatabaseNames.Join(",")));
                }

                targetDatabaseName = value;
            }
        }
        private string targetDatabaseName;

        /// <summary>The name of the database after recovery.</summary>
        [XmlElement("TargetDatabaseName")]
        public string TargetDatabaseName { get; set; }
    }

    /// <summary>
    /// Restore an Azure SQL Database.
    /// </summary>
    /// <see cref="http://msdn.microsoft.com/en-us/library/azure/dn509571.aspx"/>
    [XmlRoot("ServiceResource")]
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/restoredatabaseoperations",
        Verbs = "POST", Summary = "Restore an Azure SQL Database.")]
    public class CreateDatabaseRestoreRequest : IPost, IReturn<DatabaseRestore>
    {
        // Path

        /// <summary>The subscription ID.</summary>
        [XmlIgnore()]
        public string SubscriptionId { get; set; }

        /// <summary>The name of the server containing the database you want to restore.</summary>
        [XmlIgnore()]
        public string ServerName { get; set; }

        // Body

        /// <summary>The name of the database to restore.</summary>
        [XmlElement("SourceDatabaseName")]
        public string SourceDatabaseName { get; set; }

        /// <summary>
        /// Optional. The date and time (including milliseconds) when the 
        /// database was dropped. 
        /// (Only applies to restoring a dropped database.)</summary>
        [XmlElement("SourceDatabaseDeletionDate", IsNullable = true)]
        public DateTime? SourceDatabaseDeletionDate { get; set; }

        /// <summary>The name of the new database after restore.</summary>
        [XmlElement("TargetDatabaseName")]
        public string TargetDatabaseName { get; set; }

        /// <summary>Optional. The point in time to restore the database from.</summary>
        [XmlElement("TargetUtcPointInTime", IsNullable = true)]
        public DateTime? TargetUtcPointInTime { get; set; }
    }

    /// <summary>
    /// Gets the status of a specific operation on a database, all the 
    /// operations (history) on a database, or all the operations on all 
    /// databases on a server.
    /// 
    /// This functionality is equivalent to querying the 
    /// <see cref="http://msdn.microsoft.com/en-us/library/dn270022.aspx">sys.dm_operation_status</see> table.
    /// </summary>
    /// <see cref="http://msdn.microsoft.com/en-us/library/azure/dn720371.aspx"/>
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/databaseoperations/{OperationGuid}",
        Verbs = "GET", Summary = "Gets the status of a specific operation on a database.")]
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/databaseoperations?databaseName={DatabaseName}",
        Verbs = "GET", Summary = "Gets the status of all the operations (history) on a database.")]
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/databaseoperations?contentview=generic",
        Verbs = "GET", Summary = "Gets the status of all the operations on all databases on a server.")]
    public class DatabaseOperationStatus : IGet, IReturn<DatabaseOperationStatusDetails>
    {
        // Path

        /// <summary>
        /// The subscription ID.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// The name of the server containing the database whose 
        /// operation you want to query.
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// The database name to get the history of all operations on 
        /// that database.
        /// </summary>
        public Guid? OperationGuid { get; set; }

        /// <summary>
        /// The name of the server containing the database whose 
        /// operation you want to query.
        /// </summary>
        public string DatabaseName { get; set; }
    }

    /// <summary>
    /// Deletes an Azure SQL Database.
    /// </summary>
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/databases/{DatabaseName}",
        Verbs = "DELETE", Summary = "Deletes an Azure SQL Database.")]
    public class DeleteDatabase : IDelete, IReturn
    {
        // Path

        /// <summary>
        /// The subscription ID.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// The name of the server where the database is located.
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// The name of the database to delete.
        /// </summary>
        public string DatabaseName { get; set; }
    }

    /// <summary>
    /// Gets the details for an Azure SQL Database.
    /// </summary>
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/databases/{DatabaseName}",
        Verbs = "GET", Summary = "Gets the details for an Azure SQL Database.")]
    public class GetDatabase : IGet, IReturn<Database>
    {
        // Path

        /// <summary>
        /// The subscription ID.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// The name of the server containing the database whose details you want. 
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// The name of the database you want to inspect.
        /// </summary>
        public string DatabaseName { get; set; }
    }

    /// <summary>
    /// Gets event logs for an Azure SQL Database.
    /// </summary>
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/databases/{DatabaseName}/events",
        Verbs = "GET", Summary = "Gets the details for an Azure SQL Database.")]
    public class GetDatabaseEventLogs : IGet, IReturn<EventLog>
    {
        // Path

        /// <summary>
        /// The subscription ID.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// The name of the server that contains the database.
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// The name of the database copy you want.
        /// </summary>
        public string DatabaseName { get; set; }


        /// <summary>
        /// The starting date and time of the events to retrieve in UTC format, for example '2011-09-28 16:05:00'.
        /// </summary>
        [Alias("startDate")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The number of minutes of log entries to retrieve.
        /// </summary>
        [Alias("intervalSizeInMinutes")]
        [ApiAllowableValues("intervalSizeInMinutes", Values = new string[] { "5", "60", "1440" }, Type = "integer")]
        public int? IntervalSizeInMinutes { get; set; }

        /// <summary>
        /// The event type of the log entries to retrieve.
        /// </summary>
        [Alias("eventTypes")]
        [Default(typeof(string), "")]
        [ApiAllowableValues("eventTypes", Values = new string[] { "", "connection_successful", "connection_failed", "connection_terminated", "deadlock", "throttling", "throttling_long_transaction" }, Type = "string")]
        public string EventTypes { get; set; }
    }


    [Route("",
        Verbs = "GET", Summary = "")]
    public class GetDatabaseRestoreRequest : IGet
    { }

    [Route("",
        Verbs = "GET", Summary = "")]
    public class ListDatabases : IGet
    { }


    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/databases/{DatabaseName}",
        Verbs = "PUT", Summary = "Updates existing database details.")]
    public class UpdateDatabase : IPut
    { }

    #endregion

    #region Database Copy Requests

    /// <summary>
    /// Gets details for an Azure SQL Database copy.
    /// </summary>
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/databases/{DatabaseName}/databasecopies/{LinkID}",
        Verbs = "GET", Summary = "Gets the details for an Azure SQL Database.")]
    public class GetDatabaseCopy : IGet, IReturn<DatabaseCopy>
    {
        // Path

        /// <summary>
        /// The subscription ID.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// The name of the server that contains the database.
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// The name of the database copy you want.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// The link ID (the link ID is the Name element from the <see cref="https://msdn.microsoft.com/en-us/library/azure/dn509570.aspx"/>Get Database 
        /// Copy</see>, <see cref="https://msdn.microsoft.com/en-us/library/azure/dn509568.aspx">List Database Copies</see>, <see cref="https://msdn.microsoft.com/en-us/library/azure/dn509576.aspx">Start Database Copy</see>, and <see cref="https://msdn.microsoft.com/en-us/library/azure/dn509572.aspx">Update 
        /// Database Copy </see> responses).
        /// </summary>
        public string LinkName { get; set; }
    }

    [Route("",
        Verbs = "GET", Summary = "")]
    public class ListDatabaseCopies : IGet
    { }

    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/databases/{DatabaseName}/databasecopies",
    Verbs = "POST", Summary = "Starts a database copy.")]
    public class StartDatabaseCopy : IPost
    { }

    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/databases/{DatabaseName}/databasecopies/{LinkID}",
        Verbs = "DELETE", Summary = "Stops a database copy.")]
    public class StopDatabaseCopy : IDelete
    { }

    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/databases/{DatabaseName}/databasecopies/{LinkID}",
    Verbs = "PUT", Summary = "Updates the details of an Azure SQL Database copy.")]
    public class UpdateDatabaseCopy : IPut
    { }

    #endregion

    #region Firewall Requests

    /// <summary>
    /// Creates an Azure SQL Database server firewall rule.
    /// </summary>
    /// <see cref="http://msdn.microsoft.com/en-us/library/azure/dn505712.aspx"/>
    [XmlRoot("ServiceResource")]
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/firewallrules",
        Verbs = "POST", Summary = "Creates an Azure SQL Database server firewall rule.")]
    public class CreateFirewallRule : IPost, IReturn<FirewallRule>
    {
        // Path

        /// <summary>The subscription ID.</summary>
        [XmlIgnore()]
        public string SubscriptionId { get; set; }

        /// <summary>The server name.</summary>
        [XmlIgnore()]
        public string ServerName { get; set; }

        // Body

        /// <summary>The name of the new firewall rule.</summary>
        [XmlElement("Name", IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// The lowest IP address in the range of the server-level firewall 
        /// setting. IP addresses equal to or greater than this can attempt to 
        /// connect to the server. 
        /// The lowest possible IP address is 0.0.0.0.
        /// </summary>
        [XmlElement("StartIPAddress", IsNullable = false)]
        public string StartIPAddress { get; set; }

        /// <summary>
        /// The highest IP address in the range of the server-level firewall 
        /// setting. IP addresses equal to or less than this can attempt to 
        /// connect to the server. 
        /// The highest possible IP address is 255.255.255.255.
        /// </summary>
        [XmlElement("EndIPAddress", IsNullable = false)]
        public string EndIPAddress { get; set; }
    }


    /// <summary>
    /// Deletes an Azure SQL Database server firewall rule.
    /// </summary>
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/firewallrules/{FirewallRuleName}",
        Verbs = "DELETE", Summary = "Deletes an Azure SQL Database server firewall rule.")]
    public class DeleteFirewallRule : IDelete, IReturn
    {
        // Path

        /// <summary>
        /// The subscription ID.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// The name of the server with the firewall rule you want to delete.
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// The name of the firewall rule you want to delete.
        /// </summary>
        public string FirewallRuleName { get; set; }
    }

    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/firewallrules/{FirewallRuleName}",
        Verbs = "GET", Summary = "Gets details for an Azure SQL Database Server firewall rule.")]
    public class GetFirewallRule : IGet, IReturn<FirewallRule>
    {
        // Path

        /// <summary>
        /// The subscription ID.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// The name of the server with the firewall rule you want to delete.
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// The name of the firewall rule you want to delete.
        /// </summary>
        public string FirewallRuleName { get; set; }
    }

    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/firewallrules",
    Verbs = "GET", Summary = "Retrieves the set of firewall rules for an Azure SQL Database Server.")]
    public class ListFirewallRules : IGet, IReturn<FirewallRules>
    {
        // Path

        /// <summary>
        /// The subscription ID.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// The name of the server with the firewall rules.
        /// </summary>
        public string ServerName { get; set; }
    }

    [XmlRoot("ServiceResource")]
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/firewallrules/{FirewallRuleName}",
    Verbs = "PUT", Summary = "Update a firewall rule for an Azure SQL Database server.")]
    public class SetFirewallRule : IPut, IReturn<FirewallRule>
    {
        // Path

        /// <summary>
        /// The subscription ID.
        /// </summary>
        [XmlIgnore()]
        public string SubscriptionId { get; set; }

        /// <summary>
        /// The name of the server to set the firewall rule on.
        /// </summary>
        [XmlIgnore()]
        public string ServerName { get; set; }

        /// <summary>
        /// The name of the firewall rule.
        /// </summary>
        [XmlIgnore()]
        public string FirewallRuleName { get; set; }

        // Body

        /// <summary>The name of the firewall rule.</summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>The lowest IP address in the range of the firewall rule.</summary>
        [XmlElement("StartIPAddress")]
        public string StartIPAddress { get; set; }

        /// <summary>The highest IP address in the range of the firewall rule.</summary>
        [XmlElement("EndIPAddress")]
        public string EndIPAddress { get; set; }
    
    }

    #endregion

    #region Quotas

    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/serverquotas/{QuotaName}",
        Verbs = "GET", Summary = "Gets a quota for an Azure SQL Database Server.")]
    public class GetQuota : IGet, IReturn<Quota>
    {
        public string SubscriptionId { get; set; }

        public string ServerName { get; set; }

        public string QuotaName { get; set; }
    }

    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/serverquotas",
    Verbs = "GET", Summary = "Gets quotas for an Azure SQL Database Server.")]
    public class ListQuotas : IGet, IReturn<Quotas>
    {
        public string SubscriptionId { get; set; }

        public string ServerName { get; set; }
    }

    #endregion








    
    /// <summary>
    /// Returns the details for a recoverable Azure SQL Database.
    /// </summary>
    /// <see cref="https://msdn.microsoft.com/en-us/library/azure/dn800985.aspx"/>
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/recoverabledatabases/{DatabaseName}",
        Verbs = "GET", Summary = "Returns the details for a recoverable Azure SQL Database.")]
    public class GetRecoverableDatabase : IGet
    {
        // Path

        /// <summary>The subscription ID.</summary>
        public string SubscriptionId { get; set; }

        /// <summary>The server name.</summary>
        public string ServerName { get; set; }

        /// <summary>The database name.</summary>
        public string DatabaseName { get; set; }
    }

    [Route("",
        Verbs = "GET", Summary = "")]
    public class GetRestorableDroppedDatabase : IGet
    { }

    /// <summary>
    /// Gets the event logs for an Azure SQL Database Server.
    /// </summary>
    /// <see cref="https://msdn.microsoft.com/en-us/library/azure/dn505726.aspx" />
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/events",
        Verbs = "GET", Summary = "Gets the event logs for an Azure SQL Database Server.")]
    public class GetServerEventLogs : IGet, IReturn<EventLogs>
    {
        // Path

        /// <summary>The subscription ID.</summary>
        public string SubscriptionId { get; set; }

        /// <summary>The server name.</summary>
        public string ServerName { get; set; }
    
        // Parameters

        [Alias("startDate")]
        public DateTime? StartDate { get; set; }

        [Alias("intervalSizeInMinutes")]
        public int? IntervalSizeInMinutes { get; set; }

        [Alias("eventTypes")]
        public string[] EventTypes { get; set; }
    }

    /// <summary>
    /// Gets details for an Azure SQL Database Server service level objective.
    /// </summary>
    /// <see cref="https://msdn.microsoft.com/en-us/library/azure/dn505709.aspx"/>
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/serviceobjectives/{ServiceObjectiveId}",
        Verbs = "GET", Summary = "Gets details for an Azure SQL Database Server service level objective.")]
    public class GetServiceLevelObjective : IGet, IReturn<ServiceObjective>
    { 
        // Path

        /// <summary>The subscription ID.</summary>
        public string SubscriptionId { get; set; }

        /// <summary>The server name.</summary>
        public string ServerName { get; set; }

        /// <summary>The service objective ID.</summary>
        public string ServiceObjectiveId { get; set; }
    }

    /// <summary>
    /// Gets a service objective dimension setting for an Azure SQL Database Server.
    /// </summary>
    /// <see cref="https://msdn.microsoft.com/en-us/library/azure/dn505703.aspx" />
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/dimensionsettings/{DimensionSettingId}",
        Verbs = "GET", Summary = "Gets a service objective dimension setting for an Azure SQL Database Server.")]
    public class GetServiceObjectiveDimensionSetting : IGet, IReturn<DimensionSetting>
    {
        // Path

        /// <summary>The subscription ID.</summary>
        public string SubscriptionId { get; set; }

        /// <summary>The server name.</summary>
        public string ServerName { get; set; }

        /// <summary>The dimension setting ID.</summary>
        public string DimensionSettingId { get; set; }
    
    }

    // TODO: Bad documentation, fix return type
    /// <summary>
    /// Gets a list of all recoverable databases on an Azure SQL Database server.
    /// </summary>
    /// <see cref="https://msdn.microsoft.com/en-us/library/azure/dn800984.aspx" />
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/recoverabledatabases?contentview=generic",
        Verbs = "GET", Summary = "Gets a list of all recoverable databases on an Azure SQL Database server.")]
    public class ListRecoverableDatabases : IGet, IReturn<RecoverableDatabase>
    {
        // Path

        /// <summary>The subscription ID.</summary>
        public string SubscriptionId { get; set; }

        /// <summary>The server name.</summary>
        public string ServerName { get; set; }
    }


    [Route("",
        Verbs = "GET", Summary = "")]
    public class ListRestorableDroppedDatabases : IGet
    { }

    [Route("",
        Verbs = "GET", Summary = "")]
    public class ListServerDatabaseRestoreRequests : IGet
    { }


    /// <summary>
    /// Gets the service level objectives for a Microsoft Azure SQL Database Server.
    /// </summary>
    /// <see cref="https://msdn.microsoft.com/en-us/library/azure/dn505723.aspx"/>
    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/serviceobjectives",
        Verbs = "GET", Summary = "Gets the service level objectives for a Microsoft Azure SQL Database Server.")]
    public class ListServiceLevelObjectives : IGet, IReturn<ServiceLevelObjectives>
    {
        // Path

        /// <summary>The subscription ID.</summary>
        public string SubscriptionId { get; set; }

        /// <summary>The server name.</summary>
        public string ServerName { get; set; }
    }

    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}/dimensionsettings",
        Verbs = "GET", Summary = "Gets service objective dimension settings for a Microsoft Azure SQL Database Server.")]
    public class ListServiceObjectiveDimensionSettings : IGet
    { }



    [Route("/{SubscriptionId}/services/sqlservers/servers/{ServerName}?op=ResetPassword",
        Verbs = "POST", Summary = "Reset the administrator password for a server.")]
    public class SetServerAdministratorPassword : IPost
    { }





    #region Import/Export Database Requests

    [XmlRoot("StatusInput", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.SqlServer.Management.Dac.ServiceTypes")]
    [Route("/Status",
    Verbs = "POST", Summary = "Returns the status of an Import/Export operation on an Azure SQL Database.")]
    public class GetImportExportDatabaseStatus : IPost, IReturn<StatusInfos>, IDacRequest
    {
        public GetImportExportDatabaseStatus(DataCenter location)
        {
            DataCenter = location;
        }

        [XmlIgnore()]
        public DataCenter DataCenter { get; set; }

        [XmlElement("RequestId")]
        public string RequestId { get; set; }

        [XmlElement("ServerName")]
        public string ServerName { get; set; }

        [XmlElement("UserName")]
        public string UserName { get; set; }

        [XmlElement("Password")]
        public string Password { get; set; }
    }


    [XmlRoot("ExportInput", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.SqlServer.Management.Dac.ServiceTypes")]
    [Route("/Export", 
        Verbs = "POST", Summary = "Exports an Azure SQL Database to Blob storage as a BACPAC file.")]
    public class ExportDatabase : IGet, IReturn<OperationStatusGuid>, IDacRequest
    {
        public ExportDatabase(DataCenter location)
        {
            DataCenter = location;
            BlobCredentials = new BlobCredentials();
            ConnectionInfo = new ConnectionInfo();
        }

        [XmlIgnore()]
        public DataCenter DataCenter { get; set; }

        [XmlElement("BlobCredentials")]
        public BlobCredentials BlobCredentials { get; set; }

        [XmlElement("ConnectionInfo")]
        public ConnectionInfo ConnectionInfo { get; set; }
    }

    [XmlRoot("ExportInput", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.SqlServer.Management.Dac.ServiceTypes")]
    [Route("/Import",
        Verbs = "POST", Summary = "Imports a BACPAC file into a Microsoft Azure SQL Database.")]
    public class ImportDatabase : IPost, IReturn<OperationStatusGuid>, IDacRequest
    {
        public ImportDatabase(DataCenter location)
        {
            DataCenter = location;
            BlobCredentials = new BlobCredentials();
            ConnectionInfo = new ConnectionInfo();
        }

        [XmlIgnore()]
        public DataCenter DataCenter { get; set; }

        [XmlElement("AzureEdition")]
        public string AzureEdition { get; set; }

        [XmlElement("BlobCredentials", DataType = "BlobStorageAccessKeyCredentials")]
        public BlobCredentials BlobCredentials { get; set; }

        [XmlElement("ConnectionInfo")]
        public ConnectionInfo ConnectionInfo { get; set; }

        [XmlElement("DatabaseSizeInGB")]
        public int DatabaseSizeInGB { get; set; }
    }


    public class BlobCredentials
    {
        [XmlElement("URI")]
        public string URI { get; set; }

        [XmlElement("StorageAccessKey")]
        public string StorageAccessKey { get; set; }
    }


    public class ConnectionInfo
    {
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

        [XmlElement("DatabaseName")]
        public string DatabaseName { get; set; }

        [XmlElement("Password")]
        public string Password { get; set; }

        [XmlElement("UserName")]
        public string UserName { get; set; }
    }




    [XmlRoot("guid", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/")]
    public class OperationStatusGuid
    {
        [XmlText()]
        public Guid Guid { get; set; }
    }

    public interface IDacRequest
    {
        DataCenter DataCenter { get; set; }
    }

    #endregion
}
