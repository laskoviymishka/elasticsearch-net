﻿using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(FieldNameQueryJsonConverter<MatchPhrasePrefixQuery>))]
	public interface IMatchPhrasePrefixQuery : IFieldNameQuery
	{
		[JsonProperty(PropertyName = "query")]
		string Query { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string Analyzer { get; set; }

		[JsonProperty(PropertyName = "prefix_length")]
		int? PrefixLength { get; set; }

		[JsonProperty(PropertyName = "max_expansions")]
		int? MaxExpansions { get; set; }
	}

	public class MatchPhrasePrefixQuery : FieldNameQueryBase, IMatchPhrasePrefixQuery
	{
		protected override bool Conditionless => IsConditionless(this);

		public string Analyzer { get; set; }
		public int? MaxExpansions { get; set; }
		public int? PrefixLength { get; set; }
		public string Query { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.MatchPhrasePrefix = this;

		internal static bool IsConditionless(IMatchPhrasePrefixQuery q) => q.Field.IsConditionless() || q.Query.IsNullOrEmpty();
	}

	public class MatchPhrasePrefixQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<MatchPhrasePrefixQueryDescriptor<T>, IMatchPhrasePrefixQuery, T>, IMatchPhrasePrefixQuery
		where T : class
	{
		protected override bool Conditionless => MatchPhrasePrefixQuery.IsConditionless(this);

		string IMatchPhrasePrefixQuery.Query { get; set; }
		string IMatchPhrasePrefixQuery.Analyzer { get; set; }
		int? IMatchPhrasePrefixQuery.PrefixLength { get; set; }
		int? IMatchPhrasePrefixQuery.MaxExpansions { get; set; }

		public MatchPhrasePrefixQueryDescriptor<T> Query(string query) => Assign(a => a.Query = query);

		public MatchPhrasePrefixQueryDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public MatchPhrasePrefixQueryDescriptor<T> PrefixLength(int? prefixLength) => Assign(a => a.PrefixLength = prefixLength);

		public MatchPhrasePrefixQueryDescriptor<T> MaxExpansions(int? maxExpansions) => Assign(a => a.MaxExpansions = maxExpansions);
	}
}
