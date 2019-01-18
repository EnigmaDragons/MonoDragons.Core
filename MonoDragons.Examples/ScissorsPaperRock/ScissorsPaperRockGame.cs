using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Examples.ScissorsPaperRock
{
    public class RockPaperScissorsGame : ClickUiScene
    {
        private readonly Queue<SelectionMade> _queuedSelections = new Queue<SelectionMade>();
        private readonly int _playerId = Rng.Int();
        private RockPaperScissorsPhase _phase;
        private IReadOnlyCollection<SelectionMade> _selections = new List<SelectionMade>();
        private UiImage _mySelection;
        private UiImage _opponentSelection;
        private Label _winnerLabel;

        public override void Init()
        {
            Event.Subscribe<SelectionMade>(s => _queuedSelections.Enqueue(s), this);
            _mySelection = new UiImage { Image = "none", Transform = new Transform2(new Vector2(100, 100), new Size2(200, 400)), IsActive = () => _phase == RockPaperScissorsPhase.Resolving };
            _opponentSelection = new UiImage { Image = "none", Transform = new Transform2(new Vector2(500, 100), new Size2(200, 400)), IsActive = () => _phase == RockPaperScissorsPhase.Resolving };
            _winnerLabel = new Label { Text = "", IsVisible = () => _phase.Equals(RockPaperScissorsPhase.Resolving) };
            Add(new ExpandingImageButton("rock", "rock", "rock", new Transform2(new Vector2(100, 100), new Size2(200, 400)), new Size2(10, 10), () => Select(RPSOption.Rock), () => _phase.Equals(RockPaperScissorsPhase.Selecting)));
            Add(new ExpandingImageButton("paper", "paper", "paper", new Transform2(new Vector2(350, 100), new Size2(200, 400)), new Size2(10, 10), () => Select(RPSOption.Paper), () => _phase.Equals(RockPaperScissorsPhase.Selecting)));
            Add(new ExpandingImageButton("scissors", "scissors", "scissors", new Transform2(new Vector2(600, 100), new Size2(200, 400)), new Size2(10, 10), () => Select(RPSOption.Scissors), () => _phase.Equals(RockPaperScissorsPhase.Selecting)));
            Add(new Label { Text = "Waiting On Opponent", IsVisible = () => _phase.Equals(RockPaperScissorsPhase.Waiting)});
            Add(Buttons.Text("New Game", new Point(250, 10), () => _phase = RockPaperScissorsPhase.Selecting, () => _phase.Equals(RockPaperScissorsPhase.Resolving)));
            Add(_winnerLabel);
            Add(_mySelection);
            Add(_opponentSelection);
            Add(new ActionAutomaton(ProcessSelections));
        }
        
        

        private void Select(RPSOption rpsOption)
        {
            _phase = RockPaperScissorsPhase.Waiting;
            Event.Publish(new SelectionMade { Id = _playerId, Selection = rpsOption });
        }

        private void ProcessSelections()
        {
            if (_phase.Equals(RockPaperScissorsPhase.Resolving))
                return;

            while (_queuedSelections.Count > 0)
            {
                var s = _queuedSelections.Dequeue();
                _selections = _selections.Append(s);
                var selectionBox = s.Id.Equals(_playerId) ? _mySelection : _opponentSelection;
                selectionBox.Image = s.Selection.ToString().ToLower();
                if (_selections.Count == 2)
                    BeginResolving();
            }
        }

        private void BeginResolving()
        {
            _phase = RockPaperScissorsPhase.Resolving;

            var winner = WinnerId();
            _winnerLabel.Text = winner == -1 ? "Tie" : winner == _playerId ? "You won!" : "You lost!";
            _selections = new List<SelectionMade>();
        }

        private int WinnerId()
        {
            var opp = _selections.First(x => !x.Id.Equals(_playerId));
            var mySelection = _selections.First(x => x.Id.Equals(_playerId)).Selection;
            var oppSelection = opp.Selection;
            int winnerId;
            if (mySelection == oppSelection)
                winnerId = -1;
            else if (mySelection == RPSOption.Rock && oppSelection == RPSOption.Scissors)
                winnerId = _playerId;
            else if (mySelection == RPSOption.Paper && oppSelection == RPSOption.Rock)
                winnerId = _playerId;
            else if (mySelection == RPSOption.Scissors && oppSelection == RPSOption.Paper)
                winnerId = _playerId;
            else
                winnerId = opp.Id;
            return winnerId;
        }
    }

    public sealed class SelectionMade
    {
        public int Id { get; set; }
        public RPSOption Selection { get; set; }
    }
    
    public enum RockPaperScissorsPhase
    {
        Selecting,
        Waiting,
        Resolving
    }

    public enum RPSOption
    {
        Rock,
        Paper,
        Scissors
    }
}
