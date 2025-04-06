using TemperatureConverterTask.Models;
using TemperatureConverterTask.Views;

namespace TemperatureConverterTask.Controllers;

class TemperatureConversionController(ITemperatureConverter model, TemperatureConverterView view)
{
    private ITemperatureConverter _model = model;

    private TemperatureConverterView _view = view;

    public void OnChangeInputScale(object sender, EventArgs e)
    {
        ComboBox senderComboBox = (ComboBox)sender;

        _view.conversionScalesComboBox.DataSource = _view.scales.Where(s => s.Scale != (TemperatureScale)senderComboBox.SelectedValue!).ToList();
    }
}
