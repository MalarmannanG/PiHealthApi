using System;
using System.Collections.Generic;
using System.Text;
using PiHealth.DataModel;
using PiHealth.DataModel.Entity;

namespace PiHealth.Services
{
    public class AuditLogServices
    {
        public readonly IRepository<AuditLog> _repository;
        public AuditLogServices(IRepository<AuditLog> repository)
        {
            _repository = repository;
        }

        public virtual AuditLog Add(AuditLog entity)
        {
            return _repository.Insert(entity);
        }
        public virtual AuditLog InsertLog(string ControllerName, string ActionName, string UserAgent, string RequestIP, long userid, string value1 = null, string value2 = null)
        {
            AuditLog oAuditLog = new AuditLog()
            {
                Action = ActionName,
                Controller = ControllerName,
                UserAgent = UserAgent,
                IP = RequestIP,
                UserID = userid.ToString(),
                Time = DateTime.UtcNow,
                Value1 = value1,
                Value2 = value2
            };
            Add(oAuditLog);
            return oAuditLog;
        }
    }
}
