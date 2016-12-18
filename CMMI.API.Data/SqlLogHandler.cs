using CMMI.API.Data.Context;
using CMMI.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMI.API.Data
{
    public class SQLLogHandler : LogHandlerBase, IDisposable
    {
        public SQLLogHandler(LogHandlerBase handler, int tracelevel, string application) : base(handler, tracelevel)
        {
        }

        public void Dispose()
        {

        }

        public override void HandleError(Exception ex)
        {
            try
            {
                using (var db = new CMMIDemoData())
                {
                    db.SysErrors.Add(BuildError(ex, DateTime.Now));
                    db.SaveChanges();
                }
            }
            catch (Exception) { }
        }

        private SysError BuildError(Exception ex, DateTime occured)
        {
            var ety = new SysError()
            {
                Exception = ex.GetType().Name,
                Message = ex.Message,
                Stack = ex.StackTrace,
                CreatedDate = occured
            };
            if (ex is DbEntityValidationException)
            {
                var vEx = (DbEntityValidationException)ex;
                var details = new StringBuilder();
                foreach (var eve in vEx.EntityValidationErrors)
                {
                    details.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    details.AppendLine();
                    foreach (var ve in eve.ValidationErrors)
                    {
                        details.AppendFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        details.AppendLine();
                    }
                }
                ety.Details = details.ToString();
            }
            if (ex.InnerException != null)
                ety.SysErrors1.Add(BuildError(ex.InnerException, occured));
            return ety;

        }

        public override void HandleTrace(int lvl, string logType, string message)
        {
            try
            {
                using (var db = new CMMIDemoData())
                {
                    var ety = new SysTrace()
                    {
                        Level = lvl,
                        LogType = logType,
                        Message = message,
                        CreatedDate = DateTime.Now
                    };
                    db.SysTraces.Add(ety);
                    db.SaveChanges();
                }
            }
            catch (Exception) { }
        }
    }
}
