using System;
using Ankiro.Framework.Tools.Bus;
using EventSourcingTest.Interfaces;
using FoossballPlayars.Commands;
using FoossballPlayars.Events;

namespace FoossballPlayars.Services
{
    public interface IGameService:  Handles<RegisterPlayarCommand>, Handles<PlayGameCommand>
    {
    }
}