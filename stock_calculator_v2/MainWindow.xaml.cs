using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data;
using System.Net;
using System.Timers;
using System.Globalization;

namespace stock_calculator_v2
{
    //Data Grid Colors   

    //Add Trade History

    //Change Update to Clear

    //Add delete row field

    //Setup Backup File incase C\StockCalculatorData.txt cannot be created

    //Combine Multiple Trades of Same Symbol   

    //Finish DataGrid Calculations

    //Publish

    public partial class MainWindow : Window
    {
        Boolean calcEntryCheck;
        Boolean portfolioEntryCheck;
        Boolean DCAEntryCheck;
        Boolean DCAImportCheck;
        String path = @"C:\StockCalculatorData.txt";
        double shares;
        double purchasePrice;
        double buyCommission;
        double totalBuyCommission;
        double totalSharePurchased;
        double totalPricetoShare;
        double lastP;

        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            tradeTypeComboBox.Items.Add("Buy");
            tradeTypeComboBox.Items.Add("Sell");
            fileCheck();
            portfolioSelectAllSetting();
        }

        private void portfolioAddButton_Click(object sender, RoutedEventArgs e)
        {
            checkPortfolioInvalidEntry();

            if (portfolioEntryCheck == true)
            {
                String symbol = portfolioSymbolBox.Text;
                String notes = portfolioNotesBox.Text;
                String tradeType = tradeTypeComboBox.SelectionBoxItem.ToString();
                String date = datePicker.Text;
                String sharePrice = portfolioPriceBox.Text;
                String share = portfolioSharesBox.Text;
                String commission = portfolioCBox.Text;

                TextWriter tw = new StreamWriter(path, true);
                tw.Write(symbol + ",");
                tw.Write(tradeType + ",");
                tw.Write(date + ",");
                tw.Write(sharePrice + ",");
                tw.Write(share + ",");
                tw.Write(commission + ",");
                tw.Write(notes);
                tw.WriteLine();
                tw.Close();

                portfolioSymbolBox.Text = "Symbol";
                portfolioNotesBox.Text = "Notes";
                tradeTypeComboBox.ClearValue(ComboBox.SelectedItemProperty);  //   .SelectionBoxItem.ToString();
                datePicker.ClearValue(DatePicker.SelectedDateProperty);
                portfolioPriceBox.Text = "Price";
                portfolioSharesBox.Text = "Shares";
                portfolioCBox.Text = "Commission";
            }
            else
            {
                showMessageBox("Invalid Entry Somewhere!", "Error!");
            }
        }

        private void calcCalculateButton_Click(object sender, RoutedEventArgs e)
        {

            if (calcShares.Text.Length == 0 || calcPPrice.Text.Length == 0 || calcSPrice.Text.Length == 0 || calcBC.Text.Length == 0 || calcSC.Text.Length == 0

                    || calcShares.Text.Equals(" ") || calcPPrice.Text.Equals(" ") || calcSPrice.Text.Equals(" ") || calcBC.Text.Equals(" ") || calcSC.Text.Equals(" "))
            {

                showMessageBox("Space or Nothing Found!", "Error!");

            }

            else
            {

                checkCalculatorInvalidEntry();

                if (calcEntryCheck == true)
                {

                    shares = double.Parse(calcShares.Text);

                    purchasePrice = double.Parse(calcPPrice.Text);

                    buyCommission = double.Parse(calcBC.Text);



                    double sellPrice = double.Parse(calcSPrice.Text);

                    double sellComminssion = double.Parse(calcSC.Text);

                    double totalPurchasePrice;

                    double totalSellPrice;

                    double result;

                    double breakEven;



                    totalPurchasePrice = (purchasePrice * shares) + buyCommission;

                    totalSellPrice = (sellPrice * shares) - sellComminssion;

                    result = totalSellPrice - totalPurchasePrice;



                    calcTotalPP.Text = totalPurchasePrice.ToString(".###");

                    calcTotalSP.Text = totalSellPrice.ToString(".###");

                    calcResult.Text = result.ToString(".###");

                    breakEven = (sellPrice - (result / shares));

                    calcBreakEven.Text = breakEven.ToString(".####");

                }

                else
                {

                    showMessageBox("Invalid Entry Somewhere!", "Error!");

                }



            }



        }

        private void dcaCalculate_Click(object sender, RoutedEventArgs e)
        {

            checkDCAInvalidEntry();

            if (DCAEntryCheck == true)
            {

                double totalPriceShare;

                double tOneTotal, tTwoTotal, tThreeTotal, tFourTotal, totalTot;



                double tOnePS = double.Parse(tradeOnePS.Text);

                double tTwoPS = double.Parse(tradeTwoPS.Text);

                double tThreePS = double.Parse(tradeThreePS.Text);

                double tFourPS = double.Parse(tradeFourPS.Text);



                double tOneBC = double.Parse(tradeOneBC.Text);

                double tTwoBC = double.Parse(tradeTwoBC.Text);

                double tThreeBC = double.Parse(tradeThreeBC.Text);

                double tFourBC = double.Parse(tradeFourBC.Text);



                double tOneShares = double.Parse(tradeOneShares.Text);

                double tTwoShares = double.Parse(tradeTwoShares.Text);

                double tThreeShares = double.Parse(tradeThreeShares.Text);

                double tFourShares = double.Parse(tradeFourShares.Text);





                tOneTotal = (tOnePS * tOneShares) + tOneBC;

                tTwoTotal = (tTwoPS * tTwoShares) + tTwoBC;

                tThreeTotal = (tThreePS * tThreeShares) + tThreeBC;

                tFourTotal = (tFourPS * tFourShares) + tFourBC;



                totalPriceShare = tOneTotal + tTwoTotal + tThreeTotal + tFourTotal;

                totalBuyCommission = tOneBC + tTwoBC + tThreeBC + tFourBC;

                totalSharePurchased = tOneShares + tTwoShares + tThreeShares + tFourShares;

                totalTot = tOneTotal + tTwoTotal + tThreeTotal + tFourTotal;



                tradeOneTotal.Text = tOneTotal.ToString(".###");

                tradeTwoTotal.Text = tTwoTotal.ToString(".###");

                tradeThreeTotal.Text = tThreeTotal.ToString(".###");

                tradeFourTotal.Text = tFourTotal.ToString(".###");

                totalBC.Text = totalBuyCommission.ToString(".###");

                totalShares.Text = totalSharePurchased.ToString(".###");

                totalTotal.Text = totalTot.ToString(".###");



                totalPricetoShare = (totalPriceShare / totalSharePurchased);

                totalPricePerShare.Text = totalPricetoShare.ToString(".###");

            }

            else
            {

                showMessageBox("Invalid Entry Somewhere!", "Error!");

            }





        }

        private void DCAImport_Click(object sender, RoutedEventArgs e)
        {

            checkDCAImportInvalidEntry();

            if (DCAImportCheck == true)
            {

                calculatorTab.IsSelected = true;

                calcShares.Text = totalSharePurchased.ToString(".###");

                calcPPrice.Text = totalPricetoShare.ToString(".###");

                //calcBC.Text = totalBuyCommission.ToString(".###");
                calcBC.Text = "0";
                calcSC.Text = "0";

            }

            else { showMessageBox("Something is missing or there is a letter instead of a number.", "Error!"); }



        }

        private void DCAClearButton_Click(object sender, RoutedEventArgs e)
        {

            if (tradeOnePS.Text.Length >= 0) { tradeOnePS.Text = ""; }

            if (tradeTwoPS.Text.Length >= 0) { tradeTwoPS.Text = ""; }

            if (tradeThreePS.Text.Length >= 0) { tradeThreePS.Text = ""; }

            if (tradeFourPS.Text.Length >= 0) { tradeFourPS.Text = ""; }



            if (tradeOneBC.Text.Length >= 0) { tradeOneBC.Text = ""; }

            if (tradeTwoBC.Text.Length >= 0) { tradeTwoBC.Text = ""; }

            if (tradeThreeBC.Text.Length >= 0) { tradeThreeBC.Text = ""; }

            if (tradeFourBC.Text.Length >= 0) { tradeFourBC.Text = ""; }



            if (tradeOneShares.Text.Length >= 0) { tradeOneShares.Text = ""; }

            if (tradeTwoShares.Text.Length >= 0) { tradeTwoShares.Text = ""; }

            if (tradeThreeShares.Text.Length >= 0) { tradeThreeShares.Text = ""; }

            if (tradeFourShares.Text.Length >= 0) { tradeFourShares.Text = ""; }

        }

        private void calcClearButton_Click(object sender, RoutedEventArgs e)
        {

            calcShares.Text = "";

            calcPPrice.Text = "";

            calcSPrice.Text = "";

            calcBC.Text = "";

            calcSC.Text = "";

        }



        //------------------------------------------One Time Edit Methods Below------------------------------------------------------------

        private void fileCheck()
        {

            if (!File.Exists(path))
            {
                try
                {
                    File.Create(path);
                }
                catch (Exception e)
                {
                    showMessageBox(e.ToString(), "FileCreationError");
                }
            }

        }

        private void checkCalculatorInvalidEntry()
        {

            double num;

            String str;

            try
            {

                num = double.Parse(calcShares.Text);

                str = num.ToString();

                num = double.Parse(calcPPrice.Text);

                str = num.ToString();

                num = double.Parse(calcSPrice.Text);

                str = num.ToString();

                num = double.Parse(calcBC.Text);

                str = num.ToString();

                num = double.Parse(calcSC.Text);

                str = num.ToString();

                calcEntryCheck = true;

            }



            catch (Exception exc)
            {

                calcEntryCheck = false;

                str = exc.Message;

            }

        }

        private void checkPortfolioInvalidEntry()
        {

            double num;

            String str;

            try
            {

                num = double.Parse(portfolioPriceBox.Text);

                str = num.ToString();

                num = double.Parse(portfolioSharesBox.Text);

                str = num.ToString();

                num = double.Parse(portfolioCBox.Text);

                str = num.ToString();

                portfolioEntryCheck = true;

            }



            catch (Exception exc)
            {

                portfolioEntryCheck = false;

                str = exc.Message;

            }

        }

        private void checkDCAInvalidEntry()
        {

            double num;

            String str;

            try
            {

                if (tradeOnePS.Text.Length == 0) { tradeOnePS.Text = "0"; }

                if (tradeTwoPS.Text.Length == 0) { tradeTwoPS.Text = "0"; }

                if (tradeThreePS.Text.Length == 0) { tradeThreePS.Text = "0"; }

                if (tradeFourPS.Text.Length == 0) { tradeFourPS.Text = "0"; }



                if (tradeOneBC.Text.Length == 0) { tradeOneBC.Text = "0"; }

                if (tradeTwoBC.Text.Length == 0) { tradeTwoBC.Text = "0"; }

                if (tradeThreeBC.Text.Length == 0) { tradeThreeBC.Text = "0"; }

                if (tradeFourBC.Text.Length == 0) { tradeFourBC.Text = "0"; }



                if (tradeOneShares.Text.Length == 0) { tradeOneShares.Text = "0"; }

                if (tradeTwoShares.Text.Length == 0) { tradeTwoShares.Text = "0"; }

                if (tradeThreeShares.Text.Length == 0) { tradeThreeShares.Text = "0"; }

                if (tradeFourShares.Text.Length == 0) { tradeFourShares.Text = "0"; }



                num = double.Parse(tradeOnePS.Text);

                str = num.ToString();

                num = double.Parse(tradeTwoPS.Text);

                str = num.ToString();

                num = double.Parse(tradeThreePS.Text);

                str = num.ToString();

                num = double.Parse(tradeFourPS.Text);

                str = num.ToString();

                num = double.Parse(tradeOneBC.Text);

                str = num.ToString();

                num = double.Parse(tradeTwoBC.Text);

                str = num.ToString();

                num = double.Parse(tradeThreeBC.Text);

                str = num.ToString();

                num = double.Parse(tradeFourBC.Text);

                str = num.ToString();

                num = double.Parse(tradeOneShares.Text);

                str = num.ToString();

                num = double.Parse(tradeTwoShares.Text);

                str = num.ToString();

                num = double.Parse(tradeThreeShares.Text);

                str = num.ToString();

                num = double.Parse(tradeFourShares.Text);

                str = num.ToString();

                DCAEntryCheck = true;

            }



            catch (Exception exc)
            {

                DCAEntryCheck = false;

                str = exc.Message;

            }

        }

        private void checkDCAImportInvalidEntry()
        {

            double num;

            String str;

            try
            {

                num = double.Parse(totalTotal.Text);

                str = num.ToString();

                num = double.Parse(totalShares.Text);

                str = num.ToString();

                num = double.Parse(totalBC.Text);

                str = num.ToString();

                num = double.Parse(totalPricePerShare.Text);

                str = num.ToString();

                DCAImportCheck = true;

            }



            catch (Exception exc)
            {

                DCAImportCheck = false;

                str = exc.Message;

            }

        }

        private void showMessageBox(String Message, String Title)
        {

            MessageBox.Show(Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private List<Author> LoadCollectionData()
        {
            List<Author> authors = new List<Author>();
            String line;

            // Read the file and display it line by line.
            StreamReader file = new StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                String[] main = line.Split(',');     //Deliminates File         
                double shares = Double.Parse(main[4]), pp = Double.Parse(main[3]), com = Double.Parse(main[5]);
                string serverURL = "http://download.finance.yahoo.com/d/quotes.csv?s=" + main[0] + "&f=sl1d1t1c1ohgv&e=.csv";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverURL);

                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII))
                        {
                            string html = reader.ReadToEnd();
                           // if (html.Contains("N/A"))
                              //  throw new ArgumentException("The symbol '" + main[0] + "' does not exist.");
                                                       
                                CultureInfo format = new CultureInfo("en-US");
                                string[] args = html.Split(',');
                                lastP = double.Parse(args[1], format);                      
                        }
                    }
                }
                catch (WebException)
                {
                    showMessageBox("Last Price Look Up Error", "Last Price Look Up Error");
                }


                //Change Cell Color
                //dataGrid[0, 0].Style.BackColor = Color.Red;
                
                
                //End Change Cell Color


                authors.Add(new Author()
                 {
                     Symbol = main[0],
                     Type = main[1],
                     Date = main[2],
                     PurchasePrice = Double.Parse(main[3]),// Double.Parse("Parse Substring"),
                     Shares = Double.Parse(main[4]),// Double.Parse("Parse Substring"),
                     Commission = Double.Parse(main[5]),// Double.Parse("Parse Substring"),
                     LastPrice = lastP, // Double, get from internet
                     OverallReturn = ((lastP * shares) - ((pp * shares) + com)), // Double =(lastp * shares)-((pp * shares) + com) // Does not Include Sell Commission
                     notes = main[6] // Substring
                 });
            }
            file.Close();
            return authors;
        }

        public class Author
        {
            public String Symbol { get; set; }
            public String Type { get; set; }
            public String Date { get; set; }
            public double PurchasePrice { get; set; }
            public double Shares { get; set; }
            public double Commission { get; set; }
            public double LastPrice { get; set; }
            public double OverallReturn { get; set; }
            public String notes { get; set; }
        }

        private void portfolioLoadButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = LoadCollectionData();
        }

        public void portfolioSelectAllSetting()
        {
            portfolioSymbolBox.SelectionStart = 0;
            portfolioSymbolBox.SelectionLength = portfolioSymbolBox.Text.Length;
            portfolioNotesBox.SelectionStart = 0;
            portfolioNotesBox.SelectionLength = portfolioNotesBox.Text.Length;
            portfolioPriceBox.SelectionStart = 0;
            portfolioPriceBox.SelectionLength = portfolioPriceBox.Text.Length;
            portfolioSharesBox.SelectionStart = 0;
            portfolioSharesBox.SelectionLength = portfolioSharesBox.Text.Length;
            portfolioCBox.SelectionStart = 0;
            portfolioCBox.SelectionLength = portfolioCBox.Text.Length;
        }

        /* public static StockEngine Execute(string symbol)
         {
             StockEngine engine = new StockEngine(symbol, false);
             HttpWebRequest request = engine.CreateRequest();
             engine.ProcessResponse(request);
             return engine;
         }*/


    }

}



/*authors.Add(new Author()

            {

                Symbol = "Substring for Symbol",

                Type = "Substring for Type",

                Date = "Substring for Date",

                PurchasePrice = 1,// Double.Parse("Parse Substring"),

                Shares = 1,// Double.Parse("Parse Substring"),

                Commission = 1,// Double.Parse("Parse Substring"),

                LastPrice = 1, // Double, get from internet

                OverallReturn = 1, // Double =(lastprice * shares)-(price * shares) + commission // Does not Include Sell Commission

                notes = "Notes" // Substring

            });*/





//string serverURL = "http://download.finance.yahoo.com/d/quotes.csv?s=" + Symbol + "&f=sl1d1t1c1ohgv&e=.csv";