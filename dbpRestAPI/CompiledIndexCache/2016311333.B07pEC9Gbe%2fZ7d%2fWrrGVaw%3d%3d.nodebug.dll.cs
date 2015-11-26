using Raven.Abstractions;
using Raven.Database.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using Raven.Database.Linq.PrivateExtensions;
using Lucene.Net.Documents;
using System.Globalization;
using System.Text.RegularExpressions;
using Raven.Database.Indexing;

public class Index_Auto_PortfolioBooks_ByTrimmedIdSortByTrimmedId : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_PortfolioBooks_ByTrimmedIdSortByTrimmedId()
	{
		this.ViewText = @"from doc in docs.PortfolioBooks
select new {
	TrimmedId = doc.TrimmedId
}";
		this.ForEntityNames.Add("PortfolioBooks");
		this.AddMapDefinition(docs => 
			from doc in ((IEnumerable<dynamic>)docs)
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "PortfolioBooks", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				TrimmedId = doc.TrimmedId,
				__document_id = doc.__document_id
			});
		this.AddField("TrimmedId");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("TrimmedId");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("TrimmedId");
		this.AddQueryParameterForReduce("__document_id");
	}
}
