using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifier.DataAccess
{
    public class ForaDocumentStore
    {
        public IDocumentStore Store
        {
            get { return EmbedDocStore.Value; }
        }

        private static readonly Lazy<IDocumentStore> EmbedDocStore = new Lazy<IDocumentStore>(() =>
        {
            var docStore = new EmbeddableDocumentStore
            {
                DataDirectory = "Data",
                UseEmbeddedHttpServer = true
            };
            
            docStore.Initialize();
            return docStore;
        });
    }

}
