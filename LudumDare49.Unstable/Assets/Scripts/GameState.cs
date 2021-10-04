using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public Game Game;
    public Image StartPanel;
    public Image Result;
    public Button btnExit, btnEN, btnDE;
    public TextAsset json;

    private System.Random _rnd = new System.Random();
    private DecisionCollection _decisionCollection;
    private Text _txtYearCountry;
    private Text _txtPlayerName;
    private Text _txtMoneyLabel;
    private Text _txtMoney;
    private Text _txtTaxLabel;
    private Text _txtTax;
    private Text _txtGoalLabel;
    private Text _txtGoalEmission;
    private Text _txtCurrentEmission;
    private Text _txtMissingSaving;
    private Text _txtPollution;
    private Text _txtSatisfactionLabel;
    private Text _txtSocietyLabel;
    private Text _txtSocietySatisfaction;
    private Text _txtSocietyEmission;
    private Text _txtIndustryLabel;
    private Text _txtIndustrySatisfaction;
    private Text _txtIndustryEmission;
    private Text _txtEnergySectorLabel;
    private Text _txtEnergySectorSatisfaction;
    private Text _txtEnergySectorEmission;
    private Text _txtAgricultureLabel;
    private Text _txtAgricultureSatisfaction;
    private Text _txtAgricultureEmission;
    private Slider _barSociety;
    private Slider _barIndustry;
    private Slider _barEnergySector;
    private Slider _barAgriculture;

    public long _emissionGoal = 3000000;

    // Start is called before the first frame update
    void Start()
    {
        /*
        var collection = new DecisionCollection();
        collection.Decisions.Add(new Decision()
        {
            Number = 1,
            Segment = "Society",
            DescriptionDE = "Du verbietest im gesamten Land und der gesamten Wirtschaft die Verwendung von Wegwerf-Plastikprodukten und forschst nach umweltfreundlichen und nachhaltigen Alternativen.",
            DescriptionEN = "You are banning the use of disposable plastic products in the entire country and the entire economy and are researching environmentally friendly and sustainable alternatives.",
            ResultDescriptionDE = "Die Gesellschaft ist erfreut und die Gewässer und die gesamter Umwelt erholen sich langsam vom Plastikmüll. Die Industrie wird stark zurückgeworfen und muss für viel Geld auf die Alternativen umschwenken.",
            ResultDescriptionEN = "Society is delighted and the waters and the entire environment are slowly recovering from plastic waste. The industry is being thrown back heavily and has to switch to alternatives for a lot of money.",
            Costs = 1500000,
            Pollution = -7,
            SocietySatisfaction = 5,
            SocietyCO2Emission = -110000,
            IndustrySatisfaction = -5,
            IndustryCO2Emission = -100000,
            EnergySectorSatisfaction = -1,
            EnergySectorCO2Emission = 10000,
            AgricultureSatisfaction = 3,
            AgricultureCO2Emission = 0
        });
        collection.Decisions.Add(new Decision()
        {
            Number = 2,
            Segment = "EnergySector",
            DescriptionDE = "Kernkraftwerke fördern",
            DescriptionEN = "Promote nuclear power plants",
            ResultDescriptionDE = "Die Gesellschaft ist nicht erfreut, die Endlagerung ist nicht geklärt, die Industrie freut sich über die sinkenden Energiepreise.",
            ResultDescriptionEN = "Society is not happy, the final disposal has not been clarified, the industry is happy about the falling energy prices.",
            Costs = 3000000,
            Pollution = 10,
            SocietySatisfaction = -10,
            SocietyCO2Emission = 0,
            IndustrySatisfaction = 5,
            IndustryCO2Emission = 0,
            EnergySectorSatisfaction = -1,
            EnergySectorCO2Emission = -400000,
            AgricultureSatisfaction = 0,
            AgricultureCO2Emission = 0
        });
        */

        // Only for saveing the template:
        //string json = JsonUtility.ToJson(collection, true);
        //File.WriteAllText(@"D:\Projekte\LudumDare49.Unstable\Designs and Concepts\decisions.json", json);

        // var json = File.ReadAllText(@"D:\Projekte\LudumDare49.Unstable\Designs and Concepts\decisions.json");
        //TextAsset json = Resources.Load("decisions") as TextAsset;
        //var json = File.ReadAllText(@"C:\Temp\decisions.json");
        _decisionCollection = JsonUtility.FromJson<DecisionCollection>(json.text);

        //Console.WriteLine(myObject.Decisions?[0].ResultDescriptionDE);

        long StartSocietyCO2Emission = 500000;
        long StartIndustryCO2Emission = 1500000;
        long StartEnergySectorCO2Emission = 1500000;
        long StartAgricultureCO2Emission = 1500000;

        Game = new Game()
        {
            Language = "en",
            PlayerName = "Spoilerqueen",  // TODO: From player input
            CountryName = "Germany",  // TODO: From player input
            Currency = "€",  // TODO: From player input
            PreviousCO2EmissionOverall = StartSocietyCO2Emission + StartIndustryCO2Emission + StartEnergySectorCO2Emission + StartAgricultureCO2Emission,
            ListOfMadeDecisions = new List<int>(),
            ListOfSeenDecisions = new List<int>(),
            CurrentYear = 2021,
            Money = 5000000,
            EnvironmentPollutionInPercent = 50,
            StartCO2EmissionOverall = StartSocietyCO2Emission + StartIndustryCO2Emission + StartEnergySectorCO2Emission + StartAgricultureCO2Emission,
            SocietySatisfactionInPercent = 50,
            SocietyCO2Emission = StartSocietyCO2Emission,
            IndustrySatisfactionInPercent = 50,
            IndustryCO2Emission = StartIndustryCO2Emission,
            EnergySectorSatisfactionInPercent = 50,
            EnergySectorCO2Emission = StartEnergySectorCO2Emission,
            AgricultureSatisfactionInPercent = 50,
            AgricultureCO2Emission = StartAgricultureCO2Emission,
        };

        Game.TaxIncomePerRound = UpdateAndGetTaxIncome(Game);

        // TODO: Show Start Screen
        // TODO: Show Intro

        _txtYearCountry = GameObject.FindWithTag("MainCanvas").transform.Find("txtYearCountry").gameObject.GetComponent<Text>();
        _txtPlayerName = GameObject.FindWithTag("MainCanvas").transform.Find("txtPlayerName").gameObject.GetComponent<Text>();
        _txtMoneyLabel = GameObject.FindWithTag("MainCanvas").transform.Find("txtMoneyLabel").gameObject.GetComponent<Text>();
        _txtMoney = GameObject.FindWithTag("MainCanvas").transform.Find("txtMoney").gameObject.GetComponent<Text>();
        _txtTaxLabel = GameObject.FindWithTag("MainCanvas").transform.Find("txtTaxLabel").gameObject.GetComponent<Text>();
        _txtTax = GameObject.FindWithTag("MainCanvas").transform.Find("txtTax").gameObject.GetComponent<Text>();
        _txtGoalLabel = GameObject.FindWithTag("MainCanvas").transform.Find("txtGoalLabel").gameObject.GetComponent<Text>();
        _txtGoalEmission = GameObject.FindWithTag("MainCanvas").transform.Find("txtGoalEmission").gameObject.GetComponent<Text>();
        _txtCurrentEmission = GameObject.FindWithTag("MainCanvas").transform.Find("txtCurrentEmission").gameObject.GetComponent<Text>();
        _txtMissingSaving = GameObject.FindWithTag("MainCanvas").transform.Find("txtMissingSaving").gameObject.GetComponent<Text>();
        _txtPollution = GameObject.FindWithTag("MainCanvas").transform.Find("txtPollution").gameObject.GetComponent<Text>();
        _txtSatisfactionLabel = GameObject.FindWithTag("MainCanvas").transform.Find("txtSatisfactionLabel").gameObject.GetComponent<Text>();
        _txtSocietySatisfaction = GameObject.FindWithTag("MainCanvas").transform.Find("txtSocietySatisfaction").gameObject.GetComponent<Text>();
        _txtSocietyLabel = GameObject.FindWithTag("MainCanvas").transform.Find("txtSocietyLabel").gameObject.GetComponent<Text>();
        _txtSocietyEmission = GameObject.FindWithTag("MainCanvas").transform.Find("txtSocietyEmission").gameObject.GetComponent<Text>();
        _txtIndustryLabel = GameObject.FindWithTag("MainCanvas").transform.Find("txtIndustryLabel").gameObject.GetComponent<Text>();
        _txtIndustrySatisfaction = GameObject.FindWithTag("MainCanvas").transform.Find("txtIndustrySatisfaction").gameObject.GetComponent<Text>();
        _txtIndustryEmission = GameObject.FindWithTag("MainCanvas").transform.Find("txtIndustryEmission").gameObject.GetComponent<Text>();
        _txtEnergySectorLabel = GameObject.FindWithTag("MainCanvas").transform.Find("txtEnergySectorLabel").gameObject.GetComponent<Text>();
        _txtEnergySectorSatisfaction = GameObject.FindWithTag("MainCanvas").transform.Find("txtEnergySectorSatisfaction").gameObject.GetComponent<Text>();
        _txtEnergySectorEmission = GameObject.FindWithTag("MainCanvas").transform.Find("txtEnergySectorEmission").gameObject.GetComponent<Text>();
        _txtAgricultureLabel = GameObject.FindWithTag("MainCanvas").transform.Find("txtAgricultureLabel").gameObject.GetComponent<Text>();
        _txtAgricultureSatisfaction = GameObject.FindWithTag("MainCanvas").transform.Find("txtAgricultureSatisfaction").gameObject.GetComponent<Text>();
        _txtAgricultureEmission = GameObject.FindWithTag("MainCanvas").transform.Find("txtAgricultureEmission").gameObject.GetComponent<Text>();
        _barSociety = GameObject.FindWithTag("MainCanvas").transform.Find("barSociety").gameObject.GetComponent<Slider>();
        _barIndustry = GameObject.FindWithTag("MainCanvas").transform.Find("barIndustry").gameObject.GetComponent<Slider>();
        _barEnergySector = GameObject.FindWithTag("MainCanvas").transform.Find("barEnergySector").gameObject.GetComponent<Slider>();
        _barAgriculture = GameObject.FindWithTag("MainCanvas").transform.Find("barAgriculture").gameObject.GetComponent<Slider>();

        btnExit.onClick.AddListener(TaskOnClickExit);
        btnEN.onClick.AddListener(TaskOnClickEN);
        btnDE.onClick.AddListener(TaskOnClickDE);

        StartPanel.transform.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (firstStart)
        {
            GameObject.FindWithTag("MainCanvas").GetComponent<AudioSource>().Play();
            firstStart = false;
        }
        */

        // TODO: update all values in text fields
        if (Game.Language == "en")
        {
            _txtYearCountry.text = $"Year {Game.CurrentYear} in {Game.CountryName}";
            _txtPlayerName.text = $"Consultant {Game.PlayerName}";
            _txtMoneyLabel.text = $"Money:";
            _txtTaxLabel.text = $"Tax income p.a.:";
            _txtSatisfactionLabel.text = "Satisfaction";
            _txtSocietyLabel.text = "Society:";
            _txtIndustryLabel.text = "Industry:";
            _txtEnergySectorLabel.text = "Energy sector:";
            _txtAgricultureLabel.text = "Agriculture:";
            _txtGoalLabel.text = "Goal";
            _txtGoalEmission.text = $"{_emissionGoal.ToStringCO2(true)} emission till 2030 (40 % less)";
            _txtCurrentEmission.text = $"Current emission: {GetTotalCO2Emission(Game).ToStringCO2(true)}";
            _txtMissingSaving.text = $"Missing saving: {(GetTotalCO2Emission(Game) - _emissionGoal).ToStringCO2(true)}";
            _txtPollution.text = $"Environment pollution: {Game.EnvironmentPollutionInPercent} %";
        }
        else
        {
            _txtYearCountry.text = $"Jahr {Game.CurrentYear} in {Game.CountryName}";
            _txtPlayerName.text = $"Berater {Game.PlayerName}";
            _txtMoneyLabel.text = $"Geld:";
            _txtTaxLabel.text = $"Steuereinnahmen p.a.:";
            _txtSatisfactionLabel.text = "Zufriedenheit";
            _txtSocietyLabel.text = "Gesellschaft:";
            _txtIndustryLabel.text = "Industrie:";
            _txtEnergySectorLabel.text = "Energiesektor:";
            _txtAgricultureLabel.text = "Landwirtschaft:";
            _txtGoalLabel.text = "Ziel";
            _txtGoalEmission.text = $"{_emissionGoal.ToStringCO2(true)} emission bis 2030 (40 % weniger)";
            _txtCurrentEmission.text = $"Derzeitige Emission: {GetTotalCO2Emission(Game).ToStringCO2(true)}";
            _txtMissingSaving.text = $"Fehlende Ersparnis: {(GetTotalCO2Emission(Game) - _emissionGoal).ToStringCO2(true)}";
            _txtPollution.text = $"Umweltverschmutzung: {Game.EnvironmentPollutionInPercent} %";
        }

        _txtMoney.text = Game.Money.ToStringMoney(Game.Currency, Game.Language);
        _txtTax.text = Game.TaxIncomePerRound.ToStringMoney(Game.Currency, Game.Language);
        _txtSocietySatisfaction.text = Game.SocietySatisfactionInPercent.ToString() + " %";
        _barSociety.value = Convert.ToSingle(Game.SocietySatisfactionInPercent);
        _txtSocietyEmission.text = Game.SocietyCO2Emission.ToStringCO2(false);
        _txtIndustrySatisfaction.text = Game.IndustrySatisfactionInPercent.ToString() + " %";
        _barIndustry.value = Convert.ToSingle(Game.IndustrySatisfactionInPercent);
        _txtIndustryEmission.text = Game.IndustryCO2Emission.ToStringCO2(false);
        _txtEnergySectorSatisfaction.text = Game.EnergySectorSatisfactionInPercent.ToString() + " %";
        _barEnergySector.value = Convert.ToSingle(Game.EnergySectorSatisfactionInPercent);
        _txtEnergySectorEmission.text = Game.EnergySectorCO2Emission.ToStringCO2(false);
        _txtAgricultureSatisfaction.text = Game.AgricultureSatisfactionInPercent.ToString() + " %";
        _barAgriculture.value = Convert.ToSingle(Game.AgricultureSatisfactionInPercent);
        _txtAgricultureEmission.text = Game.AgricultureCO2Emission.ToStringCO2(false);
    }

    void TaskOnClickExit()
    {
        Application.Quit();
    }

    void TaskOnClickDE()
    {
        Game.Language = "de";
    }

    void TaskOnClickEN()
    {
        Game.Language = "en";
    }

    public void StartRound(Game currentGame, bool firstRound)
    {
        Result.transform.gameObject.SetActive(false);

        var decisions = GetFourDecisions(_decisionCollection.Decisions, currentGame.ListOfSeenDecisions);

        if (decisions.Count != 4)
            throw new System.Exception("Not enough decisions found!");

        UpdateSeenDecisions(currentGame, decisions);

        int counter = 1;

        foreach (var decision in decisions)
        {
            var gameObject = GameObject.FindWithTag("MainCanvas").transform.Find("pnlDecision" + counter).gameObject;

            if (gameObject == null)
                continue;

            PanelDecision panel = gameObject.GetComponent(typeof(PanelDecision)) as PanelDecision;
            panel.CurrentDecision = decision;
            gameObject.SetActive(true);

            counter++;
        }
    }

    public List<Decision> GetFourDecisions(List<Decision> allDecisions, List<int> seenDecisions)
    {
        //var currentDecisionsForSelection = new List<Decision>(allDecisions);
        var currentDecisions = new List<Decision>();

        // remove made decisions from list
        foreach (int numberOfMadeDecision in seenDecisions)
        {
            // search for action
            var decisionToRemove = allDecisions.FirstOrDefault(d => d.Number == numberOfMadeDecision);

            if (decisionToRemove != null)
                allDecisions.Remove(decisionToRemove);
        }

        currentDecisions.Add(GetRandomSegmentDecision("Society", allDecisions));
        currentDecisions.Add(GetRandomSegmentDecision("Industry", allDecisions));
        currentDecisions.Add(GetRandomSegmentDecision("EnergySector", allDecisions));
        currentDecisions.Add(GetRandomSegmentDecision("Agriculture", allDecisions));

        return currentDecisions;
    }

    private Decision GetRandomSegmentDecision(string segment, List<Decision> allDecisions)
    {
        List<Decision> segmentDecisions = allDecisions.Where(d => d.Segment == segment).ToList();
        Decision decision;

        if (segmentDecisions.Count == 0)
            throw new System.Exception($"too less {segment} decisions found!");

        if (segmentDecisions.Count == 1)
        {
            decision = segmentDecisions[0];
        }
        else
        {
            var random = _rnd.Next(0, segmentDecisions.Count);
            decision = segmentDecisions[random];
        }

        return decision;
    }

    private void UpdateSeenDecisions(Game currentGame, List<Decision> newSeenDecisions)
    {
        foreach (var decision in newSeenDecisions)
            currentGame.ListOfSeenDecisions.Add(decision.Number);
    }

    private long GetTotalCO2Emission(Game currentGame)
    {
        return currentGame.SocietyCO2Emission +
               currentGame.IndustryCO2Emission +
               currentGame.EnergySectorCO2Emission +
               currentGame.AgricultureCO2Emission;
    }

    private short GetTotalSatisfaction(Game currentGame)
    {
        return (short)(currentGame.SocietySatisfactionInPercent +
               currentGame.IndustrySatisfactionInPercent +
               currentGame.EnergySectorSatisfactionInPercent +
               currentGame.AgricultureSatisfactionInPercent);
    }

    private long UpdateAndGetTaxIncome(Game currentGame)
    {
        currentGame.TaxIncomePerRound = GetTotalSatisfaction(currentGame) * 7000;  // calculate tax income
        return currentGame.TaxIncomePerRound;
    }

    public void SetDecisionsActive(bool value)
    {
        for (int i = 1; i < 5; i++)
        {
            var gameObject = GameObject.FindWithTag("MainCanvas").transform.Find("pnlDecision" + i).gameObject;
            gameObject.SetActive(value);
        }
    }

    public void MakeDecision(Game currentGame, Decision decision)
    {
        currentGame.Money -= decision.Costs;
        currentGame.EnvironmentPollutionInPercent += decision.Pollution;
        currentGame.SocietySatisfactionInPercent += decision.SocietySatisfaction;
        currentGame.SocietyCO2Emission += decision.SocietyCO2Emission;
        currentGame.IndustrySatisfactionInPercent += decision.IndustrySatisfaction;
        currentGame.IndustryCO2Emission += decision.IndustryCO2Emission;
        currentGame.EnergySectorSatisfactionInPercent += decision.EnergySectorSatisfaction;
        currentGame.EnergySectorCO2Emission += decision.EnergySectorCO2Emission;
        currentGame.AgricultureSatisfactionInPercent += decision.AgricultureSatisfaction;
        currentGame.AgricultureCO2Emission += decision.AgricultureCO2Emission;

        currentGame.ListOfMadeDecisions.Add(decision.Number);

        SetDecisionsActive(false);

        var headline = Result.transform.GetChild(0);
        var description = Result.transform.GetChild(1);
        var effects = Result.transform.GetChild(2);
        var pollution = Result.transform.GetChild(3);

        NewRound(currentGame);

        if (currentGame.Language == "en")
        {
            headline.gameObject.GetComponent<Text>().text = "Result of the year";
            description.gameObject.GetComponent<Text>().text = decision.ResultDescriptionEN;

            var costsText = "Costs: " + decision.Costs.ToStringMoney(Game.Currency, Game.Language) + Environment.NewLine;

            if (decision.Pollution > 0)
                costsText += Environment.NewLine + "Pollution: +" + decision.Pollution.ToString();
            else if (decision.Pollution < 0)
                costsText += Environment.NewLine + "Pollution: " + decision.Pollution.ToString();

            if (decision.SocietySatisfaction > 0)
                costsText += Environment.NewLine + "Society satisfaction: +" + decision.SocietySatisfaction.ToString() + " %";
            else if (decision.SocietySatisfaction < 0)
                costsText += Environment.NewLine + "Society satisfaction: " + decision.SocietySatisfaction.ToString() + " %";

            if (decision.SocietyCO2Emission > 0)
                costsText += Environment.NewLine + "Society emission: +" + decision.SocietyCO2Emission.ToStringCO2(true);
            else if (decision.SocietyCO2Emission < 0)
                costsText += Environment.NewLine + "Society emission: " + decision.SocietyCO2Emission.ToStringCO2(true);

            if (decision.IndustrySatisfaction > 0)
                costsText += Environment.NewLine + "Industry satisfaction: +" + decision.IndustrySatisfaction.ToString() + " %";
            else if (decision.IndustrySatisfaction < 0)
                costsText += Environment.NewLine + "Industry satisfaction: " + decision.IndustrySatisfaction.ToString() + " %";

            if (decision.IndustryCO2Emission > 0)
                costsText += Environment.NewLine + "Industry emission: +" + decision.IndustryCO2Emission.ToStringCO2(true);
            else if (decision.IndustryCO2Emission < 0)
                costsText += Environment.NewLine + "Industry emission: " + decision.IndustryCO2Emission.ToStringCO2(true);

            if (decision.EnergySectorSatisfaction > 0)
                costsText += Environment.NewLine + "Energy sector satisfaction: +" + decision.EnergySectorSatisfaction.ToString() + " %";
            else if (decision.EnergySectorSatisfaction < 0)
                costsText += Environment.NewLine + "Energy sector satisfaction: " + decision.EnergySectorSatisfaction.ToString() + " %";

            if (decision.EnergySectorCO2Emission > 0)
                costsText += Environment.NewLine + "Energy sector emission: +" + decision.EnergySectorCO2Emission.ToStringCO2(true);
            else if (decision.EnergySectorCO2Emission < 0)
                costsText += Environment.NewLine + "Energy sector emission: " + decision.EnergySectorCO2Emission.ToStringCO2(true);

            if (decision.AgricultureSatisfaction > 0)
                costsText += Environment.NewLine + "Agriculture satisfaction: +" + decision.AgricultureSatisfaction.ToString() + " %";
            else if (decision.AgricultureSatisfaction < 0)
                costsText += Environment.NewLine + "Agriculture satisfaction: " + decision.AgricultureSatisfaction.ToString() + " %";

            if (decision.AgricultureCO2Emission > 0)
                costsText += Environment.NewLine + "Agriculture emission: +" + decision.AgricultureCO2Emission.ToStringCO2(true);
            else if (decision.AgricultureCO2Emission < 0)
                costsText += Environment.NewLine + "Agriculture emission: " + decision.AgricultureCO2Emission.ToStringCO2(true);

            // show values from pollution
            var pollutionText = "Satisfaction change from pollution";
            var value = currentGame.EnvironmentPollutionInPercent;

            if (value < 40)
            {
                currentGame.SocietySatisfactionInPercent += 5;
                currentGame.AgricultureSatisfactionInPercent += 4;

                pollutionText += Environment.NewLine + Environment.NewLine + "Society satisfaction: +5 %";
                pollutionText += Environment.NewLine + "Agriculture satisfaction: +4 %";
            }
            else if (value >= 40 && value < 50)
            {
                currentGame.SocietySatisfactionInPercent += 3;
                currentGame.AgricultureSatisfactionInPercent += 2;

                pollutionText += Environment.NewLine + Environment.NewLine + "Society satisfaction: +3 %";
                pollutionText += Environment.NewLine + "Agriculture satisfaction: +2 %";
            }
            else if (value >= 60 && value < 70)
            {
                currentGame.SocietySatisfactionInPercent += -2;
                currentGame.IndustrySatisfactionInPercent += 1;
                currentGame.EnergySectorSatisfactionInPercent += 1;
                currentGame.AgricultureSatisfactionInPercent += -2;

                pollutionText += Environment.NewLine + Environment.NewLine + "Society satisfaction: -2 %";
                pollutionText += Environment.NewLine + "Industry satisfaction: +1 %";
                pollutionText += Environment.NewLine + "Energy sector satisfaction: +1 %";
                pollutionText += Environment.NewLine + "Agriculture satisfaction: -2 %";
            }
            else if (value >= 70 && value < 80)
            {
                currentGame.SocietySatisfactionInPercent += -4;
                currentGame.IndustrySatisfactionInPercent += 2;
                currentGame.EnergySectorSatisfactionInPercent += 2;
                currentGame.AgricultureSatisfactionInPercent += -4;

                pollutionText += Environment.NewLine + Environment.NewLine + "Society satisfaction: -4 %";
                pollutionText += Environment.NewLine + "Industry satisfaction: +2 %";
                pollutionText += Environment.NewLine + "Energy sector satisfaction: +2 %";
                pollutionText += Environment.NewLine + "Agriculture satisfaction: -4 %";
            }
            else if (value >= 80 && value < 90)
            {
                currentGame.SocietySatisfactionInPercent += -8;
                currentGame.IndustrySatisfactionInPercent += 5;
                currentGame.EnergySectorSatisfactionInPercent += 3;
                currentGame.AgricultureSatisfactionInPercent += -8;

                pollutionText += Environment.NewLine + Environment.NewLine + "Society satisfaction: -8 %";
                pollutionText += Environment.NewLine + "Industry satisfaction: +5 %";
                pollutionText += Environment.NewLine + "Energy sector satisfaction: +3 %";
                pollutionText += Environment.NewLine + "Agriculture satisfaction: -8 %";
            }
            else if (value >= 90)
            {
                currentGame.SocietySatisfactionInPercent += -15;
                currentGame.IndustrySatisfactionInPercent += 10;
                currentGame.EnergySectorSatisfactionInPercent += 5;
                currentGame.AgricultureSatisfactionInPercent += -15;

                pollutionText += Environment.NewLine + Environment.NewLine + "Society satisfaction: -15 %";
                pollutionText += Environment.NewLine + "Industry satisfaction: +10 %";
                pollutionText += Environment.NewLine + "Energy sector satisfaction: +5 %";
                pollutionText += Environment.NewLine + "Agriculture satisfaction: -15 %";
            }
            else
            {
                pollutionText += Environment.NewLine + Environment.NewLine + "Nothing has changed";
            }

            pollution.gameObject.GetComponent<Text>().text = pollutionText;
            effects.gameObject.GetComponent<Text>().text = costsText;
        }
        else
        {
            headline.gameObject.GetComponent<Text>().text = "Ergebnis des Jahres";
            description.gameObject.GetComponent<Text>().text = decision.ResultDescriptionDE;

            var costsText = "Kosten: " + decision.Costs.ToStringMoney(Game.Currency, Game.Language) + Environment.NewLine;

            if (decision.Pollution > 0)
                costsText += Environment.NewLine + "Umweltverschmutzung: +" + decision.Pollution.ToString();
            else if (decision.Pollution < 0)
                costsText += Environment.NewLine + "Umweltverschmutzung: " + decision.Pollution.ToString();

            if (decision.SocietySatisfaction > 0)
                costsText += Environment.NewLine + "Gesellschaft Zufriedenheit: +" + decision.SocietySatisfaction.ToString() + " %";
            else if (decision.SocietySatisfaction < 0)
                costsText += Environment.NewLine + "Gesellschaft Zufriedenheit: " + decision.SocietySatisfaction.ToString() + " %";

            if (decision.SocietyCO2Emission > 0)
                costsText += Environment.NewLine + "Gesellschaft Emission: +" + decision.SocietyCO2Emission.ToStringCO2(true);
            else if (decision.SocietyCO2Emission < 0)
                costsText += Environment.NewLine + "Gesellschaft Emission: " + decision.SocietyCO2Emission.ToStringCO2(true);

            if (decision.IndustrySatisfaction > 0)
                costsText += Environment.NewLine + "Industrie Zufriedenheit: +" + decision.IndustrySatisfaction.ToString() + " %";
            else if (decision.IndustrySatisfaction < 0)
                costsText += Environment.NewLine + "Industrie Zufriedenheit: " + decision.IndustrySatisfaction.ToString() + " %";

            if (decision.IndustryCO2Emission > 0)
                costsText += Environment.NewLine + "Industrie Emission: +" + decision.IndustryCO2Emission.ToStringCO2(true);
            else if (decision.IndustryCO2Emission < 0)
                costsText += Environment.NewLine + "Industrie Emission: " + decision.IndustryCO2Emission.ToStringCO2(true);

            if (decision.EnergySectorSatisfaction > 0)
                costsText += Environment.NewLine + "Energiesektor Zufriedenheit: +" + decision.EnergySectorSatisfaction.ToString() + " %";
            else if (decision.EnergySectorSatisfaction < 0)
                costsText += Environment.NewLine + "Energiesektor Zufriedenheit: " + decision.EnergySectorSatisfaction.ToString() + " %";

            if (decision.EnergySectorCO2Emission > 0)
                costsText += Environment.NewLine + "Energiesektor Emission: +" + decision.EnergySectorCO2Emission.ToStringCO2(true);
            else if (decision.EnergySectorCO2Emission < 0)
                costsText += Environment.NewLine + "Energiesektor Emission: " + decision.EnergySectorCO2Emission.ToStringCO2(true);

            if (decision.AgricultureSatisfaction > 0)
                costsText += Environment.NewLine + "Landwirtschaft Zufriedenheit: +" + decision.AgricultureSatisfaction.ToString() + " %";
            else if (decision.AgricultureSatisfaction < 0)
                costsText += Environment.NewLine + "Landwirtschaft Zufriedenheit: " + decision.AgricultureSatisfaction.ToString() + " %";

            if (decision.AgricultureCO2Emission > 0)
                costsText += Environment.NewLine + "Landwirtschaft Emission: +" + decision.AgricultureCO2Emission.ToStringCO2(true);
            else if (decision.AgricultureCO2Emission < 0)
                costsText += Environment.NewLine + "Landwirtschaft Emission: " + decision.AgricultureCO2Emission.ToStringCO2(true);

            // show values from pollution
            var pollutionText = "Zufriedenheitsänderung durch Verschmutzung";
            var value = currentGame.EnvironmentPollutionInPercent;

            if (value < 40)
            {
                currentGame.SocietySatisfactionInPercent += 5;
                currentGame.AgricultureSatisfactionInPercent += 4;

                pollutionText += Environment.NewLine + Environment.NewLine + "Gesellschaft Zufriedenheit: +5 %";
                pollutionText += Environment.NewLine + "Landwirtschaft Zufriedenheit: +4 %";
            }
            else if (value >= 40 && value < 50)
            {
                currentGame.SocietySatisfactionInPercent += 3;
                currentGame.AgricultureSatisfactionInPercent += 2;

                pollutionText += Environment.NewLine + Environment.NewLine + "Gesellschaft Zufriedenheit: +3 %";
                pollutionText += Environment.NewLine + "Landwirtschaft Zufriedenheit: +2 %";
            }
            else if (value >= 60 && value < 70)
            {
                currentGame.SocietySatisfactionInPercent += -2;
                currentGame.IndustrySatisfactionInPercent += 1;
                currentGame.EnergySectorSatisfactionInPercent += 1;
                currentGame.AgricultureSatisfactionInPercent += -2;

                pollutionText += Environment.NewLine + Environment.NewLine + "Gesellschaft Zufriedenheit: -2 %";
                pollutionText += Environment.NewLine + "Industrie Zufriedenheit: +1 %";
                pollutionText += Environment.NewLine + "Energiesektor Zufriedenheit: +1 %";
                pollutionText += Environment.NewLine + "Landwirtschaft Zufriedenheit: -2 %";
            }
            else if (value >= 70 && value < 80)
            {
                currentGame.SocietySatisfactionInPercent += -4;
                currentGame.IndustrySatisfactionInPercent += 2;
                currentGame.EnergySectorSatisfactionInPercent += 2;
                currentGame.AgricultureSatisfactionInPercent += -4;

                pollutionText += Environment.NewLine + Environment.NewLine + "Gesellschaft Zufriedenheit: -4 %";
                pollutionText += Environment.NewLine + "Industrie Zufriedenheit: +2 %";
                pollutionText += Environment.NewLine + "Energiesektor Zufriedenheit: +2 %";
                pollutionText += Environment.NewLine + "Landwirtschaft Zufriedenheit: -4 %";
            }
            else if (value >= 80 && value < 90)
            {
                currentGame.SocietySatisfactionInPercent += -8;
                currentGame.IndustrySatisfactionInPercent += 5;
                currentGame.EnergySectorSatisfactionInPercent += 3;
                currentGame.AgricultureSatisfactionInPercent += -8;

                pollutionText += Environment.NewLine + Environment.NewLine + "Gesellschaft Zufriedenheit: -8 %";
                pollutionText += Environment.NewLine + "Industrie Zufriedenheit: +5 %";
                pollutionText += Environment.NewLine + "Energiesektor Zufriedenheit: +3 %";
                pollutionText += Environment.NewLine + "Landwirtschaft Zufriedenheit: -8 %";
            }
            else if (value >= 90)
            {
                currentGame.SocietySatisfactionInPercent += -15;
                currentGame.IndustrySatisfactionInPercent += 10;
                currentGame.EnergySectorSatisfactionInPercent += 5;
                currentGame.AgricultureSatisfactionInPercent += -15;

                pollutionText += Environment.NewLine + Environment.NewLine + "Gesellschaft Zufriedenheit: -15 %";
                pollutionText += Environment.NewLine + "Industrie Zufriedenheit: +10 %";
                pollutionText += Environment.NewLine + "Energiesektor Zufriedenheit: +5 %";
                pollutionText += Environment.NewLine + "Landwirtschaft Zufriedenheit: -15 %";
            }
            else
            {
                pollutionText += Environment.NewLine + Environment.NewLine + "Nichts hat sich geändert";
            }

            pollution.gameObject.GetComponent<Text>().text = pollutionText;
            effects.gameObject.GetComponent<Text>().text = costsText;
        }

        Result.transform.gameObject.SetActive(true);
    }

    private void NewRound(Game currentGame)
    {
        currentGame.CurrentYear++;
        currentGame.Money += UpdateAndGetTaxIncome(currentGame);
        currentGame.PreviousCO2EmissionOverall += GetTotalCO2Emission(currentGame);
    }

    public bool IsGameWon(Game currentGame)
    {
        // winning, if it's the year 2030 and the CO2 emission overall is less or equal than 3 Mio. t
        return currentGame.CurrentYear == 2030 && GetTotalCO2Emission(currentGame) <= _emissionGoal;
    }

    public bool IsGameOver(Game currentGame, ref string cause)
    {
        // game over, if year is 2030 and the CO2 emission is bigger than 3.000.000
        if (currentGame.CurrentYear == 2030 && GetTotalCO2Emission(currentGame) > _emissionGoal)
        {
            if (currentGame.Language == "en")
                cause = "Oh no! You are doomed! You were not able to stabilize the environment, which is know striking back with all of her power. Floodings, droughts and fires destroying your country and are making it uninhabital. Unluckily, the only option for you is going to Mars now.";
            else
                cause = "Oh nein! Du bist verdammt! Du konntest die Umwelt nicht stabilisieren, die nun mit all ihren Kräften zurück schlägt. Überschwemmungen, Dürren und Brände überziehen dein Land und machen es unbewohnbar. Schade, jetzt bleibt dir nur noch der Rückzug auf den Mars. ";

            return true;
        }

        // game over, if money is less or equal null after paying the interest and getting the tax
        var newMoney = currentGame.Money;
        newMoney += UpdateAndGetTaxIncome(currentGame);

        if (newMoney <= 0)
        {
            if (currentGame.Language == "en")
                cause = "Oh oh! Your are broke! Your climate decisions were maybe a bit too ambitious, what means that you can't make any decision anymore. Your country will be disappear in the dust of CO2 emission shortly.";
            else
                cause = "Oh oh! Du bist pleite! Deine Umweltmaßnahmen waren vielleicht etwas zu ambitioniert, was zur Folge hat, dass du keinerlei Maßnahmen mehr umsetzen kannst. Dein Land wird kurzfristig im Dunst von CO2-Emissionen verschwinden.";

            return true;
        }

        // game over, if one of the segments has satisfaction less or equal to zero
        if (currentGame.SocietySatisfactionInPercent <= 0)
        {
            if (currentGame.Language == "en")
                cause = "Oh oh! Your population fled! You made them so unhappy with your decisions, that they prefer to live in the neighbour country, now. The missing workers bring also the industry, the agriculture and the energy sector as well as the state to an end. But at least nature is happy about it, which can regenerate completely.";
            else
                cause = "Oh oh! Deine Bevölkerung ist geflohen! Du hast sie mit den Maßnahmen so unglücklich gemacht, dass diese nun lieber im Nachbarland leben. Die fehlenden Arbeitskräfte bedeuten gleichzeitig auch das Aus für die Industrie, die Landwirtschaft und den Energiesektor sowie letztendlich auch für den Staat. Aber über das brachliegende Land freut sich zumindest die Natur, die sich nun vollständig erholen kann.";

            return true;
        }

        if (currentGame.IndustrySatisfactionInPercent <= 0)
        {
            if (currentGame.Language == "en")
                cause = "Oh oh! Industry left the country! Your decisions made companys unprofitable. The energy sector is shrinking, too. Many people are unemployed and starting riots. The government has to go and you are also redundant.";
            else
                cause = "Oh oh! Die Industrie hat das Land verlassen! Deine Maßnahmen haben die Unternehmen unrentabel werden lassen. Auch der Energiesektor hat sich stark verkleinert. Viele Menschen sind nun arbeitslos und gehen auf die Barrikaden. Die Regierung kann sich nicht mehr halten und auch du bis überflüssig geworden.";

            return true;
        }

        if (currentGame.EnergySectorSatisfactionInPercent <= 0)
        {
            if (currentGame.Language == "en")
                cause = "Oh oh! The lights are going out! The energy sector went to the neighbour countries. You have to import expansive and eco-unfriendly energy, now. But at least, you can reach your CO2-goal with this doubtful instrument.";
            else
                cause = "Oh oh! In deinem Land gehen die Lichter aus! Der Energiesektor ist in die Nachbarländer abgewandert. Du musst nun teure und umweltschädliche Energien importieren. Aber zumindest kannst du mit diesem zweifelhaften Mittel dein eigenes CO2-Ziel erreichen.";

            return true;
        }

        if (currentGame.AgricultureSatisfactionInPercent <= 0)
        {
            if (currentGame.Language == "en")
                cause = "Oh oh! Agriculture gave up! Your decisions made farms unprofitable. Your country has to import expansive food with enviromental unfriendly foot print. You can not reach your CO2 goal anymore.";
            else
                cause = "Oh oh! Die Landwirtschaft hat aufgegeben! Deine Maßnahmen haben die Bauernhöfe unrentabel werden lassen. Dein Land muss nun teure Lebensmittel mit umweltschädlichem Fußabdruck importieren. Dein CO2-Ziel kannst du nicht mehr erreichen.";

            return true;
        }

        return false;
    }
}
