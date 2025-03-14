#region S# License
/******************************************************************************************
NOTICE!!!  This program and source code is owned and licensed by
StockSharp, LLC, www.stocksharp.com
Viewing or use of this code requires your acceptance of the license
agreement found at https://github.com/StockSharp/StockSharp/blob/master/LICENSE
Removal of this comment is a violation of the license agreement.

Project: StockSharp.Algo.Indicators.Algo
File: AwesomeOscillator.cs
Created: 2015, 11, 11, 2:32 PM

Copyright 2010 by StockSharp, LLC
*******************************************************************************************/
#endregion S# License
namespace StockSharp.Algo.Indicators
{
	using System;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;

	using Ecng.Serialization;
	using Ecng.ComponentModel;

	using StockSharp.Localization;

	/// <summary>
	/// Awesome Oscillator.
	/// </summary>
	/// <remarks>
	/// https://doc.stocksharp.com/topics/IndicatorAwesomeOscillator.html
	/// </remarks>
	[Display(
		ResourceType = typeof(LocalizedStrings),
		Name = LocalizedStrings.AOKey,
		Description = LocalizedStrings.Str836Key)]
	[Doc("topics/IndicatorAwesomeOscillator.html")]
	public class AwesomeOscillator : BaseIndicator
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AwesomeOscillator"/>.
		/// </summary>
		public AwesomeOscillator()
			: this(new SimpleMovingAverage { Length = 34 }, new SimpleMovingAverage { Length = 5 })
		{
		}

		/// <inheritdoc />
		public override int NumValuesToInitialize => Math.Max(LongMa.NumValuesToInitialize, Math.Max(ShortMa.NumValuesToInitialize, MedianPrice.NumValuesToInitialize));

		/// <summary>
		/// Initializes a new instance of the <see cref="AwesomeOscillator"/>.
		/// </summary>
		/// <param name="longSma">Long moving average.</param>
		/// <param name="shortSma">Short moving average.</param>
		public AwesomeOscillator(SimpleMovingAverage longSma, SimpleMovingAverage shortSma)
		{
			ShortMa = shortSma ?? throw new ArgumentNullException(nameof(shortSma));
			LongMa = longSma ?? throw new ArgumentNullException(nameof(longSma));
			MedianPrice = new MedianPrice();
		}

		/// <inheritdoc />
		public override IndicatorMeasures Measure => IndicatorMeasures.MinusOnePlusOne;

		/// <summary>
		/// Long moving average.
		/// </summary>
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.Str798Key,
			Description = LocalizedStrings.Str799Key,
			GroupName = LocalizedStrings.GeneralKey)]
		public SimpleMovingAverage LongMa { get; }

		/// <summary>
		/// Short moving average.
		/// </summary>
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.Str800Key,
			Description = LocalizedStrings.Str799Key,
			GroupName = LocalizedStrings.GeneralKey)]
		public SimpleMovingAverage ShortMa { get; }

		/// <summary>
		/// Median price.
		/// </summary>
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.Str843Key,
			Description = LocalizedStrings.Str745Key,
			GroupName = LocalizedStrings.GeneralKey)]
		public MedianPrice MedianPrice { get; }

		/// <inheritdoc />
		protected override bool CalcIsFormed() => LongMa.IsFormed;

		/// <inheritdoc />
		protected override IIndicatorValue OnProcess(IIndicatorValue input)
		{
			var mpValue = MedianPrice.Process(input);

			var sValue = ShortMa.Process(mpValue).GetValue<decimal>();
			var lValue = LongMa.Process(mpValue).GetValue<decimal>();

			return new DecimalIndicatorValue(this, sValue - lValue);
		}

		/// <inheritdoc />
		public override void Load(SettingsStorage storage)
		{
			base.Load(storage);

			LongMa.LoadNotNull(storage, nameof(LongMa));
			ShortMa.LoadNotNull(storage, nameof(ShortMa));
			MedianPrice.LoadNotNull(storage, nameof(MedianPrice));
		}

		/// <inheritdoc />
		public override void Save(SettingsStorage storage)
		{
			base.Save(storage);

			storage.SetValue(nameof(LongMa), LongMa.Save());
			storage.SetValue(nameof(ShortMa), ShortMa.Save());
			storage.SetValue(nameof(MedianPrice), MedianPrice.Save());
		}
	}
}
