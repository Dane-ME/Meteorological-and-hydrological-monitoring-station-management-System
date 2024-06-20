using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Models
{
    public class StationDetailModel
    {
        #region Chart
        public string Time { get; set; }
        public double Values { get; set; }
        #endregion

        #region DataGrid Wind
        private string _ff;
        private string _dd;
        private string _fxfx2m;
        private string _dxdx2m;
        private string _tgxh2m;
        private string _fxfx2s;
        private string _dxdx2s;
        private string _tgxh2s;
        private string _timing;

        public string ff
        {
            get { return _ff; }
            set { this._ff = value; }
        }
        public string dd
        {
            get { return _dd; }
            set { this._dd = value; }
        }
        public string fxfx2m
        {
            get => _fxfx2m;
            set => this._fxfx2m = value;
        }
        public string dxdx2m
        {
            get => _dxdx2m;
            set => this._dxdx2m = value; 
        }
        public string tgxh2m
        {
            get => this._tgxh2m;
            set { this._tgxh2m = value;}
        }
        public string fxfx2s
        {
            get => _fxfx2s; 
            set => this._fxfx2s = value;
        }
        public string dxdx2s
        {
            get => _dxdx2s;
            set => this._dxdx2s = value;
        }
        public string tgxh2s
        {
            get => _tgxh2s; 
            set => this._tgxh2s = value;
        }
        public string timing
        {
            get => _timing;
            set { this._timing = value; }
        }
        #endregion

        #region DataGrid Sea

        private string _waterlevel;
        private string _waveheight;
        private string _wavelength;
        private string _waveheightmax;

        public string waterlevel
        {
            get { return _waterlevel; }
            set { this._waterlevel = value; }
        }
        public string waveheight
        {
            get { return _waveheight; }
            set { this._waveheight = value; }
        }
        public string wavelength
        {
            get { return _wavelength; }
            set { this._wavelength = value; }
        }
        public string waveheightmax
        {
            get { return _waveheightmax; }
            set { this._waveheightmax = value; }
        }


        #endregion
        public StationDetailModel(string xValue, double yValue)
        {
            Time = xValue;
            Values = yValue;
        }
        public StationDetailModel(string timing, string ff, string dd, string fxfx2m, string dxdx2m, string tgxh2m, string fxfx2s, string dxdx2s, string tgxh2s) 
        {
            this.timing = timing;
            this.ff = ff;
            this.dd = dd;
            this.fxfx2m = fxfx2m;
            this.dxdx2m = dxdx2m;
            this.tgxh2m = tgxh2m;
            this.fxfx2s = fxfx2s;
            this.dxdx2s = dxdx2s;
            this.tgxh2s = tgxh2s;
        }
        public StationDetailModel(string timing, string waterlevel, string waveheight, string wavelength, string waveheightmax)
        {
            this.timing= timing;
            this.waterlevel = waterlevel;
            this.waveheight = waveheight;
            this.wavelength = wavelength;
            this.waveheightmax = waveheightmax;
        } 
    }
}
namespace System
{
    public partial class Document
    {
        //public List<Document> StationDetailList { get => GetArray<List<Document>>(nameof(StationDetailList)); set => Push(nameof(StationDetailList), value); }

    }
}
