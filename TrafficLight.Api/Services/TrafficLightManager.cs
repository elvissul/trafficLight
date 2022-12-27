using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLight.Api.Services
{
    public interface ITrafficLightManager {
        int Ticks { get; set; }
        void Reset();
        void Stop();
        void Resume();
        DateTime TimerStarted { get; set; }
        bool IsTimerStarted { get; set; }
        void PrepareTimer(Action action);
        void Execute(object? stateInfo);
    }

    public class TrafficLightManager : ITrafficLightManager
    {
        private Timer? _timer;
        private AutoResetEvent? _autoResetEvent;
        private Action? _action;
        public DateTime TimerStarted { get; set; }
        public bool IsTimerStarted { get; set; }
        public int Ticks { get; set; } = 0;

        public void PrepareTimer(Action action)
        {
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, _autoResetEvent, 1000, 1000);
            TimerStarted = DateTime.Now;
            IsTimerStarted = true;
        }
        public void Reset() {
            Ticks = 0;
        }
        public void Resume()
        {
            IsTimerStarted = true;
            _timer = new Timer(Execute, _autoResetEvent, 1000, 1000);
        }
        public void Stop()
        {
            IsTimerStarted = false;
            Ticks = 0;
            _timer.Dispose();
        }
        public void Execute(object? stateInfo)
        {
            this.Ticks++;
            _action();
            
            //if ((DateTime.Now - TimerStarted).TotalSeconds > 60)
            //{
            //    IsTimerStarted = false;
            //    _timer.Dispose();
            //}
        }
    }
}
