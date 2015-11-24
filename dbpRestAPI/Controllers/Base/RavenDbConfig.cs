using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dbpRestAPI
{
    using Raven.Client;
    using Raven.Client.Embedded;
    using Raven.Client.Indexes;
    using System.Reflection;

    public class RavenDbConfig
    {
        private static IDocumentStore _store;
        public static IDocumentStore Store
        {
            get
            {
                if (_store == null)
                    throw new InvalidOperationException(
                    "IDocumentStore has not been initialized.");
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
                };
                _store.Conventions.IdentityPartsSeparator = "-";
                _store.Initialize();
                IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), Store);
                return _store;
            }
            return _store;
        }
    }
}