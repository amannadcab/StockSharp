#region S# License
/******************************************************************************************
NOTICE!!!  This program and source code is owned and licensed by
StockSharp, LLC, www.stocksharp.com
Viewing or use of this code requires your acceptance of the license
agreement found at https://github.com/StockSharp/StockSharp/blob/master/LICENSE
Removal of this comment is a violation of the license agreement.

Project: StockSharp.BusinessEntities.BusinessEntities
File: MyTrade.cs
Created: 2015, 11, 11, 2:32 PM

Copyright 2010 by StockSharp, LLC
*******************************************************************************************/
#endregion S# License
namespace StockSharp.BusinessEntities
{
	using System;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Runtime.Serialization;

	using Ecng.Common;
	using Ecng.ComponentModel;

	using StockSharp.Localization;

	/// <summary>
	/// Own trade.
	/// </summary>
	[Serializable]
	[DataContract]
	[Display(
		ResourceType = typeof(LocalizedStrings),
		Name = LocalizedStrings.Str502Key,
		Description = LocalizedStrings.Str503Key)]
	public class MyTrade : NotifiableObject
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MyTrade"/>.
		/// </summary>
		public MyTrade()
		{
		}

		/// <summary>
		/// Order, for which a trade was filled.
		/// </summary>
		[DataMember]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.OrderKey,
			Description = LocalizedStrings.Str505Key,
			GroupName = LocalizedStrings.GeneralKey,
			Order = 0)]
		public Order Order { get; set; }

		/// <summary>
		/// Trade info.
		/// </summary>
		[DataMember]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.Str506Key,
			Description = LocalizedStrings.Str507Key,
			GroupName = LocalizedStrings.GeneralKey,
			Order = 1)]
#pragma warning disable CS0618 // Type or member is obsolete
		public Trade Trade { get; set; }
#pragma warning restore CS0618 // Type or member is obsolete

		/// <summary>
		/// Commission.
		/// </summary>
		[DataMember]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.CommissionKey,
			Description = LocalizedStrings.Str160Key,
			GroupName = LocalizedStrings.StatisticsKey,
			Order = 0)]
		public decimal? Commission { get; set; }

		/// <summary>
		/// Commission currency. Can be <see lnagword="null"/>.
		/// </summary>
		public string CommissionCurrency { get; set; }

		/// <summary>
		/// Slippage in trade price.
		/// </summary>
		[DataMember]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.Str163Key,
			Description = LocalizedStrings.Str164Key,
			GroupName = LocalizedStrings.StatisticsKey,
			Order = 1)]
		public decimal? Slippage { get; set; }

		private decimal? _pnL;

		/// <summary>
		/// The profit, realized by trade.
		/// </summary>
		[DataMember]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.PnLKey,
			Description = LocalizedStrings.PnLKey + LocalizedStrings.Dot,
			GroupName = LocalizedStrings.StatisticsKey,
			Order = 2)]
		public decimal? PnL
		{
			get => _pnL;
			set
			{
				if (_pnL == value)
					return;

				_pnL = value;
				NotifyChanged();
			}
		}

		/// <summary>
		/// The position, generated by trade.
		/// </summary>
		[DataMember]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.Str862Key,
			Description = LocalizedStrings.Str862Key + LocalizedStrings.Dot,
			GroupName = LocalizedStrings.StatisticsKey,
			Order = 2)]
		public decimal? Position { get; set; }

		/// <summary>
		/// Used to identify whether the order initiator is an aggressor or not in the trade.
		/// </summary>
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.InitiatorKey,
			Description = LocalizedStrings.InitiatorTradeKey,
			GroupName = LocalizedStrings.GeneralKey,
			Order = 3)]
		public bool? Initiator { get; set; }

		/// <summary>
		/// Yield.
		/// </summary>
		[DataMember]
		public decimal? Yield { get; set; }

		/// <inheritdoc />
		public override string ToString()
		{
			return LocalizedStrings.Str509Params.Put(Trade, Order);
		}
	}
}