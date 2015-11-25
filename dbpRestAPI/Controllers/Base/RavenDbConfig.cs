using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dbpRestAPI
{
    using Raven.Client;
    using Raven.Client.Embedded;
    using Raven.Client.Indexes;
    using Raven.Database.Server;
    using System.Reflection;

    public class RavenDbConfig
    {
        private static IDocumentStore _store;
        public static IDocumentStore Store
        {
            get
            {
                if (_store == null)
                {
                    Initialize();
                }
                return _store;
            }
        }

        public static IDocumentStore Initialize()
        {
            
            if (_store == null)
            {
                _store = new EmbeddableDocumentStore
                {
                    ConnectionStringName = "RavenDB"
                    //,UseEmbeddedHttpServer = true
                };
                _store.Conventions.IdentityPartsSeparator = "-";
                try
                {
                    NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);
                    _store.Initialize();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }

                IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), Store);
                return _store;
            }
            return _store;
        }
    }
}