using Application.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;
using System.Collections.ObjectModel;
using UI.Storages;

namespace UI.ViewModels
{
    public partial class PieChartViewModel : ObservableObject
    {

        private readonly TransactionStorage _mutualSimpleTransactionBinding;
        public IEnumerable<ISeries> IncomeByCategorySeries { get; set; }
        public IEnumerable<ISeries> ExpenseByCategorySeries { get; set; }
        public LabelVisual IncomeByCategoryTitle { get; set; }
        public LabelVisual ExpenseByCategoryTitle { get; set; }
        public PieChartViewModel(TransactionStorage mutualSimpleTransactionBinding)
        {
            _mutualSimpleTransactionBinding = mutualSimpleTransactionBinding;

            IncomeByCategoryStats();
            ExpenseByCategoryStats();
        }

        [RelayCommand]
        async void Appear() => await OnAppear();

        public async Task OnAppear()
        {
            await _mutualSimpleTransactionBinding.Reload();
        }

        void IncomeByCategoryStats()
        {
            var query = _mutualSimpleTransactionBinding.SimpleTransactions.Where(st => st.Type == 0).GroupBy(st => st.TransactionCategory.Name);
            var result = query.Select(g => new
            {
                Name = g.Key,
                Count = g.Count()
            });
            IncomeByCategorySeries = result.AsLiveChartsPieSeries((value, series) =>
            {
                series.Name = $"{value.Name}";
                series.Mapping = (value, p) =>
                {
                    p.PrimaryValue = value.Count;
                };
                series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
                series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle;
                series.DataLabelsFormatter = p => $"{p.StackedValue.Share:P2}";
                series.DataLabelsSize = 40;
            });
            IncomeByCategoryTitle = new LabelVisual
            {
                Text = "Income by category",
                TextSize = 40,
                Padding = new Padding(15),
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
            };
        }
        void ExpenseByCategoryStats()
        {
            var query = _mutualSimpleTransactionBinding.SimpleTransactions.Where(st => st.Type == 1).GroupBy(st => st.TransactionCategory.Name);
            var result = query.Select(g => new
            {
                Name = g.Key,
                Count = g.Count()
            });
            ExpenseByCategorySeries = result.AsLiveChartsPieSeries((value, series) =>
            {
                series.Name = $"{value.Name}";
                series.Mapping = (value, p) =>
                {
                    p.PrimaryValue = value.Count;
                };
                series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
                series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle;
                series.DataLabelsFormatter = p => $"{p.StackedValue.Share:P2}";
                series.DataLabelsSize = 40;
            });
            ExpenseByCategoryTitle = new LabelVisual
            {
                Text = "Expense by category",
                TextSize = 40,
                Padding = new Padding(15),
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
            };
        }

    }
}
