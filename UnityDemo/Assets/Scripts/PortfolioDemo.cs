using System.Collections.Generic;
using UnityEngine;

public sealed class PortfolioDemo : DemoCanvas
{
    protected override string Title => "Project Nemesis";
    protected override string Subtitle => "Draft upgrades. Collect three from each corporation to unlock a fusion.";
    protected override Color Accent => new Color32(233, 121, 229, 255);

    private static readonly string[] Names = {
        "Arc Capacitor", "Ion Field", "Pulse Reactor", "Phase Coil", "Signal Boost",
        "Nano Shield", "Alloy Skin", "Servo Drive", "Titan Core", "Guard Matrix"
    };

    private readonly bool[] _owned = new bool[Names.Length];
    private readonly List<int> _options = new List<int>();
    private System.Random _random = new System.Random(19);
    private bool _fusion;

    private void Start()
    {
        ResetDraft();
    }

    private int CountOwned(int company)
    {
        int count = 0;
        for (int i = company * 5; i < company * 5 + 5; i++)
        {
            if (_owned[i]) { count++; }
        }

        return count;
    }

    private bool FusionAvailable => !_fusion && CountOwned(0) >= 3 && CountOwned(1) >= 3;

    private void Draft()
    {
        _options.Clear();
        var pool = new List<int>();
        for (int i = 0; i < Names.Length; i++)
        {
            if (!_owned[i]) { pool.Add(i); }
        }

        while (_options.Count < 3 && pool.Count > 0)
        {
            int total = 0;
            foreach (int id in pool) { total += 1 + CountOwned(id / 5); }
            int roll = _random.Next(total);
            int candidate = 0;
            foreach (int id in pool)
            {
                roll -= 1 + CountOwned(id / 5);
                if (roll < 0)
                {
                    candidate = id;
                    break;
                }
            }

            pool.Remove(candidate);
            _options.Add(candidate);
        }
    }

    private bool Choose(int id)
    {
        if (!_options.Contains(id) || _owned[id]) { return false; }
        _owned[id] = true;
        Draft();
        return true;
    }

    private void ResetDraft()
    {
        System.Array.Clear(_owned, 0, _owned.Length);
        _random = new System.Random(19);
        _fusion = false;
        Draft();
    }

    protected override void DrawDemo()
    {
        Fill(new Rect(34, 125, 892, 115), Panel);
        Label(new Rect(56, 148, 420, 64), $"ARC LABS\nOwned {CountOwned(0)}/5   |   Weight {CountOwned(0) + 1}", Accent);
        Label(new Rect(500, 148, 400, 64), $"TITAN WORKS\nOwned {CountOwned(1)}/5   |   Weight {CountOwned(1) + 1}", Pale);
        Label(new Rect(34, 266, 700, 32), "DRAFT ONE UPGRADE", Pale);
        for (int i = 0; i < _options.Count; i++)
        {
            int id = _options[i];
            string company = id < 5 ? "Arc Labs" : "Titan Works";
            if (Button(new Rect(34 + 301 * i, 310, 286, 78), $"{company}\n{Names[id]}"))
            {
                Choose(id);
                break;
            }
        }

        if (Button(new Rect(34, 420, 284, 52), _fusion ? "Fusion claimed" : "Claim fusion upgrade", FusionAvailable))
        {
            _fusion = true;
        }

        if (Button(new Rect(342, 420, 185, 52), "Reset draft")) { ResetDraft(); }
        Label(new Rect(34, 490, 892, 34), _fusion ? "Fusion unlocked: Arc + Titan." : "A claimed upgrade is removed from the draft pool.", Pale);
    }

    public override void CheckScenario()
    {
        ResetDraft();
        var played = new HashSet<int>();
        while (_options.Count > 0)
        {
            int id = _options[0];
            Require(played.Add(id) && Choose(id), "Owned upgrade appeared twice");
        }

        Require(played.Count == Names.Length, "Draft ended with unused options");
        Require(FusionAvailable, "Fusion not offered with both prerequisites");
        _fusion = true;
        Require(!FusionAvailable, "Fusion offered twice");
        ResetDraft();
    }
}
