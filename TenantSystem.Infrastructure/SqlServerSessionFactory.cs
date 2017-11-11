using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TenantSystem.Infrastructure
{
    public class SqlServerSessionFactory
    {
        public static string ConnectionString
        {
            get
            {
                return @"Data Source=DESKTOP-NMGIVVB\KKDEV;Initial Catalog=TenantDB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
            }
        }

        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                MsSqlConfiguration.MsSql2012.ConnectionString(c => c.Is(ConnectionString)))
                .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();
        }
    }
}
