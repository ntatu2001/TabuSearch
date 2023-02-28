using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Data;
using System.Globalization;
using System.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;
using TabuSearchProject;

namespace ReadDataFromCSV
{
    public class Program : NewFindPlannedDate
    {
        /// <summary>
        /// Create a DataTable of Maintenance Deadlines and Tasks
        /// </summary>
        /// <returns></returns>
        public static DataTable ReadDataFromWorks()
        {
            string filePath = @"D:\Scheduling Maintenance\data input\Works.csv";
            DataTable workTable = new DataTable();
            workTable.Columns.Add("No");
            workTable.Columns.Add("Priority");
            workTable.Columns.Add("Device");
            workTable.Columns.Add("Work");
            workTable.Columns.Add("DueDate");
            workTable.Columns.Add("ExecutionTime");
            workTable.Columns.Add("ReleaseDate");
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    reader.ReadLine();
                    while ((line = reader.ReadLine()) != null)
                    {
                        DataRow newRow = workTable.NewRow();
                        newRow["No"] = line.Split(',')[0];
                        newRow["Priority"] = line.Split(',')[1];
                        newRow["Device"] = line.Split(',')[2];
                        newRow["Work"] = line.Split(',')[3];
                        newRow["DueDate"] = DateTime.ParseExact(line.Split(',')[4], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        newRow["ExecutionTime"] = int.Parse(line.Split(',')[5]);
                        newRow["ReleaseDate"] = DateTime.ParseExact(line.Split(',')[6], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        workTable.Rows.Add(newRow);
                    }

                    //Preprocessing to sort job orders, the higher the priority get the smaller the order number
                    //workTable.DefaultView.Sort = "Priority";
                    //workTable = workTable.DefaultView.ToTable();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //foreach (DataRow row in workTable.Rows)
            //{
            //    Console.WriteLine(row["No"] + " " + row["Priority"] + " " + row["Device"] + " " + row["Work"] + " " + row["DueDate"] + " " + row["ExecutionTime"] + " " + row["ReleaseDate"]);
            //}

            return workTable;
        }

        /// <summary>
        /// Create a DataTable of Device Working Hours 
        /// </summary>
        /// <returns></returns>
        public static DataTable ReadDataFromDevices()
        {
            string filePath = @"D:\Scheduling Maintenance\data input\Devices.csv";
            DataTable deviceTable = new DataTable();
            deviceTable.Columns.Add("No");
            deviceTable.Columns.Add("Device");
            deviceTable.Columns.Add("Date");
            deviceTable.Columns.Add("From1");
            deviceTable.Columns.Add("To1");
            deviceTable.Columns.Add("From2");
            deviceTable.Columns.Add("To2");
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    reader.ReadLine();
                    while ((line = reader.ReadLine()) != null)
                    {
                        DataRow newRow = deviceTable.NewRow();
                        newRow["No"] = line.Split(',')[0];
                        newRow["Device"] = line.Split(',')[1];
                        newRow["Date"] = line.Split(',')[2];
                        newRow["From1"] = line.Split(',')[3];
                        newRow["To1"] = line.Split(',')[4]; ;
                        newRow["From2"] = line.Split(',')[5];
                        newRow["To2"] = line.Split(',')[6]; ;
                        deviceTable.Rows.Add(newRow);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //foreach (DataRow row in deviceTable.Rows)
            //{
            //    Console.WriteLine(row["No"] + " " + row["Device"] + " " + row["Date"] + " " + row["From1"] + " " + row["To1"] + " " + row["From2"] + " " + row["To2"]);
            //}

            return deviceTable;
        }


        /// <summary>
        /// Create a DataTable of Technician Working Hours 
        /// </summary>
        /// <returns></returns>
        public static DataTable ReadDataFromTechnicians()
        {
            string filePath = @"D:\Scheduling Maintenance\data input\Technicians.csv";
            DataTable technicianTable = new DataTable();
            technicianTable.Columns.Add("No");
            technicianTable.Columns.Add("Technician");
            technicianTable.Columns.Add("Date");
            technicianTable.Columns.Add("From1");
            technicianTable.Columns.Add("To1");
            technicianTable.Columns.Add("From2");
            technicianTable.Columns.Add("To2");
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    reader.ReadLine();
                    while ((line = reader.ReadLine()) != null)
                    {
                        DataRow newRow = technicianTable.NewRow();
                        newRow["No"] = line.Split(',')[0];
                        newRow["Technician"] = line.Split(',')[1];
                        newRow["Date"] = line.Split(',')[2];
                        newRow["From1"] = line.Split(',')[3];
                        newRow["To1"] = line.Split(',')[4];
                        newRow["From2"] = line.Split(',')[5];
                        newRow["To2"] = line.Split(',')[6];
                        technicianTable.Rows.Add(newRow);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //foreach (DataRow row in technicianTable.Rows)
            //{
            //    Console.WriteLine(row["No"] + " " + row["Technician"] + " " + row["Date"] + " " + row["From1"] + " " + row["To1"] + " " + row["From2"] + " " + row["To2"]);
            //}

            return technicianTable;
        }


        /// <summary>
        /// Determine the length of Tabu List
        /// </summary>
        /// <param name="workTable"></param>
        /// <returns></returns>
        public static int getTenure(DataTable workTable)
        {
            int numberOfWorks = workTable.Rows.Count;
            if (numberOfWorks < 10)
            {
                return 2;
            }
            else if (numberOfWorks < 20)
            {
                return 5;
            }
            else
            {
                return 31;
            }
        }


        /// <summary>
        /// Initialize the solution in the order 1, 2, 3,..., workTable.Rows.Count
        /// </summary>
        /// <param name="workTable"></param>
        /// <returns></returns>
        public static List<int> initialSolution(DataTable workTable)
        {
            List<int> solution = new List<int>();
            int numberOfWorks = workTable.Rows.Count;
            for (int i = 1; i <= numberOfWorks; i++)
            {
                solution.Add(i);
            }

            return solution;
        }


        /// <summary>
        /// This is a subfunction to check that there are enough materials to perform maintenance at the beginning of each job
        /// Temporarily, I'll give it always true
        /// </summary>
        /// <param name="workTable"></param>
        /// <returns></returns>
        public static bool checkMaterials(DataTable workTable)
        {
            return true;
        }


        /// <summary>
        /// Calculate the value of the objective function, the objective function is the sum of the early/delay times of the maintenance orders
        /// The Value of Object Function performed by minutes
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="workTable"></param>
        /// <returns></returns>
        public static double objectValue(List<int> solution, DataTable workTable, Dictionary<string, List<List<DateTime>>> deviceDictionary, Dictionary<string, List<List<DateTime>>> technicianDictionary, Dictionary<string, List<List<DateTime>>> maintenanceDeviceBreakTime, Dictionary<string, List<List<DateTime>>> maintenanceTechnicianWorkTime)
        {
            DateTime startDate = DateTime.Now;
            double objectValue = 0;
            DateTime endDate = DateTime.Now;
            foreach (int job in solution)
            {
                List<DateTime> listStartEndWorking = findPlannedDate(workTable, solution, job, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
                startDate = listStartEndWorking[0];
                endDate = listStartEndWorking[1];

                double differenceMinutes = 0;
                DateTime dueDate = Convert.ToDateTime((string)workTable.Rows[job - 1]["DueDate"]);
                if (endDate >= dueDate)
                {
                    // The unit of value returned from Ticks property is 10^-7 ticks/second
                    differenceMinutes = TimeSpan.FromTicks((endDate - dueDate).Ticks).TotalMinutes;
                    differenceMinutes = Math.Round(differenceMinutes, 0);
                    //Console.WriteLine($"The delay time in each job = {differenceMinutes}");
                }
                else if (endDate < dueDate)
                {
                    differenceMinutes = TimeSpan.FromTicks((dueDate - endDate).Ticks).TotalMinutes;

                    differenceMinutes = Math.Round(differenceMinutes, 0);
                    //Console.WriteLine($"The early time in each job = {differenceMinutes}");
                }

                if (differenceMinutes != 0)
                {
                    objectValue += differenceMinutes;
                }
            }

            return objectValue;
        }


        /// <summary>
        /// Create a frame consisting of: Conversion Pair and Objective Function Value
        /// </summary>
        /// <param name="solution"></param>
        /// <returns></returns>
        public static Dictionary<List<int>, double> tabuStructure(List<int> solution)
        {
            //tabuAttribute(conversion pair, move value)
            Dictionary<List<int>, double> tabuAttribute = new Dictionary<List<int>, double>();

            foreach (int i in solution)
            {
                if (i < solution.Count)
                {
                    List<int> listTemp = new List<int> { i, i + 1 };
                    tabuAttribute.Add(listTemp, 0);
                }
            }

            //foreach (var kvp in tabuAttribute)
            //{
            //    Console.WriteLine(kvp.Key[0].ToString() + " - " + kvp.Key[1].ToString());
            //    Console.WriteLine(kvp.Value);
            //}

            return tabuAttribute;
        }


        /// <summary>
        /// Takes a list (solution) returns a new neighbor solution with i, j swapped
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static List<int> swapPairs(List<int> solution, int i, int j)
        {
            int iIndex = solution.IndexOf(i);
            int jIndex = solution.IndexOf(j);
            int temp = solution[iIndex];
            solution[iIndex] = solution[jIndex];
            solution[jIndex] = temp;
            return solution;
        }


        /// <summary>
        /// Check: Have the considerd pair existed in Tabu List? 
        /// If it existed, return true. Otherwise, return false
        /// </summary>
        /// <param name="bestPair"></param>
        /// <param name="tabuList"></param>
        /// <returns></returns>
        public static bool checkTabuList(List<int> bestPair, List<List<int>> tabuList)
        {
            for (int i = 0; i < tabuList.Count; i++)
            {
                if ((bestPair[0] == tabuList[i][0]) && (bestPair[1] == tabuList[i][1]))
                {
                    return true;
                }
            }
            return false;
        }

        public static List<List<int>> updateTabuList(List<int> bestMove, List<List<int>> tabuList)
        {
            if (tabuList.Count < 5)
            {
                tabuList.Add(bestMove);
            }
            else
            {
                for (int i = 0; i < (tabuList.Count - 1); i++)
                {
                    tabuList[i] = tabuList[i + 1];
                }
                tabuList[tabuList.Count - 1] = bestMove;
            }

            return tabuList;
        }


        /// <summary>
        /// Returns the swap pair have minimum objective function value in each iteration. 
        /// In addition, check: Have this pair already existed in the Tabu List? If not, add it to Tabu List!
        /// </summary>
        /// <param name="tabuAttribute"></param>
        /// <param name="tabuList"></param>
        /// <returns></returns>
        public static List<int> getBestPair(Dictionary<List<int>, double> tabuAttribute, List<List<int>> tabuList)
        {
            //Find the minimum Move Value in Tabu Attribute dictionary
            var keyMinMoveValue = tabuAttribute.MinBy(item => item.Value).Key;
            double minMoveValue = tabuAttribute[keyMinMoveValue];

            var listKey = tabuAttribute.Keys.ToList();
            var listValue = tabuAttribute.Values.ToList();

            //There can be many pairs with the same minimum value
            List<List<int>> listKeyMinValue = new List<List<int>>();
            List<int> bestPair = new List<int>();
            foreach (List<int> key in listKey)
            {
                if (tabuAttribute[key] == minMoveValue)
                {
                    listKeyMinValue.Add(key);
                }
            }

            Console.WriteLine($"The number of Pair have the same minimum Object Value: {listKeyMinValue.Count}");

            for (int index = 0; index < listKeyMinValue.Count; index++)
            {
                bestPair = listKeyMinValue[index];
                //Console.WriteLine(bestPair[0].ToString() + " - " + bestPair[1].ToString());
                if (checkTabuList(bestPair, tabuList) == false)
                {
                    //tabuList.Add(bestPair);
                    tabuList = updateTabuList(bestPair, tabuList);
                    break;
                }
            }
            return bestPair;
        }


        /// <summary>
        /// The implementation Tabu Search algorithm with short-term memory and pair swap as Tabu attribute
        /// </summary>
        /// <param name="workTable"></param>
        /// <returns></returns>
        public static List<int> tabuSearch(DataTable workTable, Dictionary<string, List<List<DateTime>>> deviceDictionary, Dictionary<string, List<List<DateTime>>> technicianDictionary)
        {
            int tenure = getTenure(workTable);
            List<List<int>> tabuList = new List<List<int>>();
            List<int> listBestPair = new List<int>();
            //Initialize Tabu List with all swap pairs [0, 0]
            //for (int i = 0; i < tenure; i++)
            //{
            //    List<int> obj = new List<int> { 0, 0 };
            //    tabuList.Add(obj);
            //}
            Dictionary<string, List<List<DateTime>>> maintenanceDeviceBreakTime = deviceStructure(deviceDictionary);
            Dictionary<string, List<List<DateTime>>> maintenanceTechnicianWorkTime = technicianStructure(technicianDictionary);

            List<int> currentSolution = initialSolution(workTable);
            double bestObjectValue = objectValue(currentSolution, workTable,deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
            List<int> bestSolution = currentSolution;

            int iterations = 50;
            int terminate = 0;
            while (terminate < iterations)
            {
                // Searching the whole neighborhood of the current solution
                Dictionary<List<int>, double> dictTabuAttribute = tabuStructure(bestSolution);

                //Calculate the objective function value corresponding to each swap pair in TabuAttribute
                var listKey = dictTabuAttribute.Keys.ToList();

                foreach (List<int> key in listKey)
                {
                    List<int> candidateSolution = swapPairs(bestSolution, key[0], key[1]);
                    //foreach (int item in candidateSolution)
                    //{
                    //    Console.Write(item + " - ");
                    //}

                    maintenanceDeviceBreakTime = deviceStructure(deviceDictionary);
                    maintenanceTechnicianWorkTime = technicianStructure(technicianDictionary);

                    double candidateObjectValue = objectValue(candidateSolution, workTable, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
                    Console.WriteLine(key[0].ToString() + " - " + key[1].ToString());
                    Console.WriteLine(candidateObjectValue);
                    dictTabuAttribute[key] = candidateObjectValue;
                }

                Console.WriteLine();
                // Print all of maintenance device's record
                foreach (string key in maintenanceDeviceBreakTime.Keys)
                {
                    Console.WriteLine($"The name of device is checked: {key}");
                    foreach (List<DateTime> listTime in maintenanceDeviceBreakTime[key])
                    {
                        Console.WriteLine(listTime[0].ToString() + " " + listTime[1].ToString());
                    }
                }

                Console.WriteLine();
                // Print all of maintenance technician's record
                foreach (string key in maintenanceTechnicianWorkTime.Keys)
                {
                    Console.WriteLine($"The sequence of technician is checked: {key}");
                    foreach (List<DateTime> listTime in maintenanceTechnicianWorkTime[key])
                    {
                        Console.WriteLine(listTime[0].ToString() + " " + listTime[1].ToString());
                    }
                }
                Console.WriteLine();

                maintenanceDeviceBreakTime = deviceStructure(deviceDictionary);
                maintenanceTechnicianWorkTime = technicianStructure(technicianDictionary);

                //Select the move with the lowest ObjValue in the neighborhood (minimization)              
                listBestPair = getBestPair(dictTabuAttribute, tabuList);
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine($"The size of ListBestPair = {listBestPair.Count}");
                Console.WriteLine($"The Best Pair : {listBestPair[0]} - {listBestPair[1]}");
                Console.WriteLine("The elements in Tabu List");

                for (int i = 0; i < tabuList.Count; i++)
                {
                    Console.Write(tabuList[i][0].ToString() + " - " + tabuList[i][1].ToString() + "||");
                }
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------------");


                if (listBestPair.Count > 0)
                {
                    currentSolution = swapPairs(bestSolution, listBestPair[0], listBestPair[1]);
                    double currentObjectValue = objectValue(currentSolution, workTable, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);

                    if (currentObjectValue < bestObjectValue)
                    {
                        bestSolution = currentSolution;
                        bestObjectValue = currentObjectValue;
                    }
                    Console.WriteLine($"The Best Pair : {listBestPair[0]} - {listBestPair[1]}");
                    Console.WriteLine($"The Object Value for Best Pair: {bestObjectValue}");
                }

                terminate += 1;
            }

            Console.WriteLine("The best Solution with the minimum Object Value");
            foreach (int item in bestSolution)
            {
                Console.Write(item + " - ");
            }
            Console.WriteLine(bestObjectValue);

            return bestSolution;
        }


        /// <summary>
        /// Returns a DataTable of Work after scheduling
        /// </summary>
        /// <param name="workTable"></param>
        /// <returns></returns>
        public static DataTable returnScheduledDataTable(DataTable workTable, Dictionary<string, List<List<DateTime>>> deviceDictionary, Dictionary<string, List<List<DateTime>>> technicianDictionary)
        {
            //Find the order of work such that the objective function value is minimal
            List<int> bestSolution = tabuSearch(workTable, deviceDictionary, technicianDictionary);

            //Create a new data table that includes the jobs sorted by bestSolution
            DataTable scheduledWorkTable = new DataTable();
            scheduledWorkTable.Columns.Add("No");
            scheduledWorkTable.Columns.Add("Priority");
            scheduledWorkTable.Columns.Add("Device");
            scheduledWorkTable.Columns.Add("Work");
            scheduledWorkTable.Columns.Add("DueDate");
            scheduledWorkTable.Columns.Add("ExecutionTime");
            scheduledWorkTable.Columns.Add("ReleaseDate");

            foreach (int job in bestSolution)
            {
                DataRow newRow = scheduledWorkTable.NewRow();
                newRow["No"] = workTable.Rows[job - 1]["No"];
                newRow["Priority"] = workTable.Rows[job - 1]["Priority"];
                newRow["Device"] = workTable.Rows[job - 1]["Device"];
                newRow["Work"] = workTable.Rows[job - 1]["Work"];
                newRow["DueDate"] = workTable.Rows[job - 1]["DueDate"];
                newRow["ExecutionTime"] = workTable.Rows[job - 1]["ExecutionTime"];
                newRow["ReleaseDate"] = workTable.Rows[job - 1]["ReleaseDate"];
                scheduledWorkTable.Rows.Add(newRow);
            }

            Console.Write("The Work Table scheduled by Tabu Search");
            foreach (DataRow row in scheduledWorkTable.Rows)
            {
                Console.WriteLine(row["No"] + " " + row["Priority"] + " " + row["Device"] + " " + row["Work"] + " " + row["DueDate"] + " " + row["ExecutionTime"] + " " + row["ReleaseDate"]);
            }

            return scheduledWorkTable;
        }

        public static void Main(string[] args)
        {
            DataTable workTable = new DataTable();
            DataTable deviceTable = new DataTable();
            DataTable technicianTable = new DataTable();

            workTable = ReadDataFromWorks();
            Console.WriteLine();
            deviceTable = ReadDataFromDevices();
            Console.WriteLine();
            technicianTable = ReadDataFromTechnicians();

            Dictionary<string, List<List<DateTime>>> deviceDictionary = getDeviceDictionary(deviceTable);
            Dictionary<string, List<List<DateTime>>> technicianDictionary = getTechnicianDictionary(technicianTable);

            //Dictionary<string, List<List<DateTime>>> maintenanceDeviceBreakTime = deviceStructure(deviceDictionary);
            //Dictionary<string, List<List<DateTime>>> maintenanceTechnicianWorkTime = technicianStructure(technicianDictionary);


            //List<int> solution = initialSolution(workTable);
            //foreach (int job in solution)
            //{
            //    List<DateTime> listStartEndWorking = findPlannedDate(workTable, solution, job, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
            //}

            DataTable scheduledWorkTable = returnScheduledDataTable(workTable, deviceDictionary, technicianDictionary);
        }
    }
}
