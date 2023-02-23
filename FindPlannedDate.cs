using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearchProject
{
    public class FindPlannedDate
    {
        public static Dictionary<string, List<List<DateTime>>> getDeviceDictionary (DataTable deviceTable)
        {
            Dictionary<string, List<List<DateTime>>> deviceDictionary = new Dictionary<string, List<List<DateTime>>>();
            List<string> listDevice = new List<string>();
            List<DateTime> listDateTimeTemp = new List<DateTime>();
            int j = 0;
            foreach (DataRow row in deviceTable.Rows)
            {
                if (j % 6 == 0)
                {
                    listDevice.Add(row["Device"].ToString());
                }

                string From1 = row["Date"] + " " + row["From1"];
                DateTime dateTimeFrom1 = DateTime.ParseExact(From1, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                listDateTimeTemp.Add(dateTimeFrom1);
                string To1 = row["Date"] + " " + row["To1"];
                DateTime dateTimeTo1 = DateTime.ParseExact(To1, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                listDateTimeTemp.Add(dateTimeTo1);
                string From2 = row["Date"] + " " + row["From2"];
                DateTime dateTimeFrom2 = DateTime.ParseExact(From2, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                listDateTimeTemp.Add(dateTimeFrom2);
                string To2 = row["Date"] + " " + row["To2"];
                DateTime dateTimeTo2 = DateTime.ParseExact(To2, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                listDateTimeTemp.Add(dateTimeTo2);
                j += 1;
            }

            List<List<DateTime>> listListTimeDevice = new List<List<DateTime>>();
            for (int i = 0; i < 24*listDevice.Count; i++)
            {
                if (i % 2 == 0)
                {
                    List<DateTime> listTemp = new List<DateTime> { listDateTimeTemp[i], listDateTimeTemp[i + 1] };
                    listListTimeDevice.Add(listTemp);
                }
            }

            for (int m = 0; m < listDevice.Count; m++)
            {
                List<List<DateTime>> listListTemp = new List<List<DateTime>>();
                for (int index = 12*m; index < 12*(m+1); index++)
                {
                    listListTemp.Add(listListTimeDevice[index]);
                }

                deviceDictionary.Add(listDevice[m], listListTemp);
            }


            //foreach (string key in deviceDictionary.Keys)
            //{
            //    Console.WriteLine($"The name of device: {key}");
            //    foreach (List<DateTime> listTime in deviceDictionary[key])
            //    {
            //        Console.WriteLine(listTime[0].ToString() + " " + listTime[1].ToString());
            //    }
            //}

            return deviceDictionary;
        }


        public static Dictionary<string, List<List<DateTime>>> getTechnicianDictionary(DataTable technicianTable)
        {
            Dictionary<string, List<List<DateTime>>> technicianDictionary = new Dictionary<string, List<List<DateTime>>>();
            List<string> listTechnician = new List<string>();
            List<DateTime> listDateTimeTemp = new List<DateTime>();
            int j = 0;
            foreach (DataRow row in technicianTable.Rows)
            {
                if (j % 6 == 0)
                {
                    listTechnician.Add(row["No"].ToString());
                }

                string From1 = row["Date"] + " " + row["From1"];
                DateTime dateTimeFrom1 = DateTime.ParseExact(From1, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                listDateTimeTemp.Add(dateTimeFrom1);
                string To1 = row["Date"] + " " + row["To1"];
                DateTime dateTimeTo1 = DateTime.ParseExact(To1, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                listDateTimeTemp.Add(dateTimeTo1);
                string From2 = row["Date"] + " " + row["From2"];
                DateTime dateTimeFrom2 = DateTime.ParseExact(From2, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                listDateTimeTemp.Add(dateTimeFrom2);
                string To2 = row["Date"] + " " + row["To2"];
                DateTime dateTimeTo2 = DateTime.ParseExact(To2, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                listDateTimeTemp.Add(dateTimeTo2);
                j += 1;
            }

            List<List<DateTime>> listListTimeDevice = new List<List<DateTime>>();
            for (int i = 0; i < 24 * listTechnician.Count; i++)
            {
                if (i % 2 == 0)
                {
                    List<DateTime> listTemp = new List<DateTime> { listDateTimeTemp[i], listDateTimeTemp[i + 1] };
                    listListTimeDevice.Add(listTemp);
                }
            }

            for (int m = 0; m < listTechnician.Count; m++)
            {
                List<List<DateTime>> listListTemp = new List<List<DateTime>>();
                for (int index = 12 * m; index < 12 * (m + 1); index++)
                {
                    listListTemp.Add(listListTimeDevice[index]);
                }

                technicianDictionary.Add(listTechnician[m], listListTemp);
            }


            //foreach (string key in technicianDictionary.Keys)
            //{
            //    Console.WriteLine($"The name of device: {key}");
            //    foreach (List<DateTime> listTime in technicianDictionary[key])
            //    {
            //        Console.WriteLine(listTime[0].ToString() + " " + listTime[1].ToString());
            //    }
            //}

            return technicianDictionary;
        }

        public static bool isInRange(DateTime startDate, DateTime endDate, DateTime checkDate)
        {
            return (startDate <= checkDate) && (checkDate <= endDate);
        }

        public static Dictionary<string, List<List<DateTime>>> deviceStructure(Dictionary<string, List<List<DateTime>>> deviceDictionary)
        {
            Dictionary<string, List<List<DateTime>>> maintenanceDeviceBreakTime = new Dictionary<string, List<List<DateTime>>>();
            foreach (string key in deviceDictionary.Keys)
            {
                List<List<DateTime>> listDateTimeEmpty = new List<List<DateTime>>();
                maintenanceDeviceBreakTime.Add(key, listDateTimeEmpty);
            }

            return maintenanceDeviceBreakTime;
        }

        public static Dictionary<string, List<List<DateTime>>> technicianStructure(Dictionary<string, List<List<DateTime>>> technicianDictionary)
        {
            Dictionary<string, List<List<DateTime>>> maintenanceTechnicianWorkTime = new Dictionary<string, List<List<DateTime>>>();
            foreach (string no in technicianDictionary.Keys)
            {
                List<List<DateTime>> listDateTimeEmpty = new List<List<DateTime>>();
                maintenanceTechnicianWorkTime.Add(no, listDateTimeEmpty);
            }

            return maintenanceTechnicianWorkTime;
        }

        public static List<int> checkTimeAvailable(string nameOfDevice, List<int> listReturn, List<DateTime> listStartEndWorking, Dictionary<string, List<List<DateTime>>> deviceDictionary, Dictionary<string, List<List<DateTime>>> technicianDictionary, Dictionary<string, List<List<DateTime>>> maintenanceDeviceBreakTime, Dictionary<string, List<List<DateTime>>> maintenanceTechnicianWorkTime)
        {
            bool checkDeviceAvailable = false;
            foreach(List<DateTime> listBreakTime in deviceDictionary[nameOfDevice])
            {
                if (isInRange(listBreakTime[0], listBreakTime[1], listStartEndWorking[0]) && isInRange(listBreakTime[0], listBreakTime[1], listStartEndWorking[1]))
                {
                    checkDeviceAvailable = true;
                    break;
                }
            }

            bool checkTechnicianAvailable = false;
            foreach(string no in technicianDictionary.Keys)
            {
                foreach(List<DateTime> listWorkTime in technicianDictionary[no])
                {
                    if (isInRange(listWorkTime[0], listWorkTime[1], listStartEndWorking[0]) && isInRange(listWorkTime[0], listWorkTime[1], listStartEndWorking[1]))
                    {
                        checkTechnicianAvailable = true;
                        break;
                    }
                }

                if (checkTechnicianAvailable && listReturn[1] == 0 )
                {
                    listReturn[1] = int.Parse(no);
                    break;
                }
            }

            if (checkDeviceAvailable && checkTechnicianAvailable)
            {
                bool checkTechnicianDuplicate = false;
                foreach(List<DateTime> listTechnicianWorkTime in maintenanceTechnicianWorkTime[listReturn[1].ToString()])
                {
                    if (isInRange(listTechnicianWorkTime[0], listTechnicianWorkTime[1], listStartEndWorking[0]) || isInRange(listTechnicianWorkTime[0], listTechnicianWorkTime[1], listStartEndWorking[1]))
                    {
                        checkTechnicianDuplicate = true;
                        break;
                    }
                }

                if (checkTechnicianDuplicate == true)
                {
                    listReturn[0] = 4;
                    return listReturn;
                }

                bool checkDeviceDuplicate = false;
                foreach (List<DateTime> listDeviceBreakTime in maintenanceDeviceBreakTime[nameOfDevice])
                {
                    if (isInRange(listDeviceBreakTime[0], listDeviceBreakTime[1], listStartEndWorking[0]) || isInRange(listDeviceBreakTime[0], listDeviceBreakTime[1], listStartEndWorking[1]))
                    {
                        checkDeviceDuplicate = true;
                        Console.WriteLine($"The duplicated date in fail 3: {listDeviceBreakTime[0]} - {listDeviceBreakTime[1]}");
                        break;
                    }
                }

                if (checkDeviceDuplicate == true)
                {
                    listReturn[0] = 3;
                    return listReturn;
                }

                if (checkTechnicianDuplicate == false && checkTechnicianDuplicate == false)
                {
                    listReturn[0] = 0;
                    return listReturn;
                }
            }
            else if (checkDeviceAvailable == false)
            {
                listReturn[0] = 1;
                return listReturn;
            }
            else if (checkTechnicianAvailable == false)
            {
                listReturn[0] = 2;
                return listReturn;
            }

            listReturn[0] = 0;
            return listReturn;
        }

        public static List<DateTime> changePlannedDate(DataTable workTable, int job, string nameOfDevice, List<DateTime> listStartEndWorking, List<int> listReturn, Dictionary<string, List<List<DateTime>>> deviceDictionary, Dictionary<string, List<List<DateTime>>> technicianDictionary, Dictionary<string, List<List<DateTime>>> maintenanceDeviceBreakTime, Dictionary<string, List<List<DateTime>>> maintenanceTechnicianWorkTime)
        {
            List<DateTime> newListStartEndWorking = new List<DateTime> { listStartEndWorking[0], listStartEndWorking[1] };

            if (listReturn[0] == 1)
            {
                Console.WriteLine($"Before change fail {listReturn[0]}, this job {job} have the start is {newListStartEndWorking[0]} and the end is {newListStartEndWorking[1]}");
                if (maintenanceDeviceBreakTime[nameOfDevice].Count == 0)
                {
                    newListStartEndWorking[0] = deviceDictionary[nameOfDevice][0][0];
                }
                else
                {
                    for (int i = 0; i < (deviceDictionary[nameOfDevice].Count - 1); i++)
                    {
                        if (listStartEndWorking[0] < deviceDictionary[nameOfDevice][0][0])
                        {
                            newListStartEndWorking[0] = deviceDictionary[nameOfDevice][0][0];
                        }
                        else if (deviceDictionary[nameOfDevice][i][0] <= listStartEndWorking[0] && listStartEndWorking[0] <= deviceDictionary[nameOfDevice][i + 1][0])
                        {
                            newListStartEndWorking[0] = deviceDictionary[nameOfDevice][i + 1][0];
                        }
                    }
                }

                double minutes = double.Parse((string)workTable.Rows[job - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                newListStartEndWorking[1] = newListStartEndWorking[0].Add(executionTime);

                Console.WriteLine($"After change fail {listReturn[0]}, this job {job} have the start is {newListStartEndWorking[0]} and the end is {newListStartEndWorking[1]}");
            }
            else if (listReturn[0] == 2)
            {
                Console.WriteLine($"Before change fail {listReturn[0]}, this job {job} have the start is {newListStartEndWorking[0]} and the end is {newListStartEndWorking[1]}");
                foreach (string no in technicianDictionary.Keys)
                {
                    if (listStartEndWorking[0] <= technicianDictionary[no][0][0])
                    {
                        Console.WriteLine("TRUE");
                        newListStartEndWorking[0] = technicianDictionary[no][0][0];
                        listReturn[1] = int.Parse(no);
                        break;
                    }
                    else
                    {
                        for (int i = 0; i < (technicianDictionary[no].Count - 1); i++)
                        {
                            if (technicianDictionary[no][i][0] <= listStartEndWorking[0] && listStartEndWorking[0] < technicianDictionary[no][i + 1][0])
                            {
                                newListStartEndWorking[0] = technicianDictionary[no][i + 1][0];
                                listReturn[1] = int.Parse(no);
                                break;
                            }
                        }
                    }  
                }

                double minutes = double.Parse((string)workTable.Rows[job - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                newListStartEndWorking[1] = newListStartEndWorking[0].Add(executionTime);
                Console.WriteLine($"After change fail {listReturn[0]}, this job {job} have the start is {newListStartEndWorking[0]} and the end is {newListStartEndWorking[1]}");
            }
            else if (listReturn[0] == 3)
            {
                Console.WriteLine($"Before change fail {listReturn[0]}, this job {job} have the start is {newListStartEndWorking[0]} and the end is {newListStartEndWorking[1]}");
                int numberOfWorkOnDevice = maintenanceDeviceBreakTime[nameOfDevice].Count;
                newListStartEndWorking[0] = maintenanceDeviceBreakTime[nameOfDevice][numberOfWorkOnDevice - 1][1].Add(TimeSpan.FromMinutes(1));
                double minutes = double.Parse((string)workTable.Rows[job - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                newListStartEndWorking[1] = newListStartEndWorking[0].Add(executionTime);
                Console.WriteLine($"After change fail {listReturn[0]}, this job {job} have the start is {newListStartEndWorking[0]} and the end is {newListStartEndWorking[1]}");
            }
            else if (listReturn[0] == 4)
            {
                Console.WriteLine($"Before change fail {listReturn[0]}, this job {job} have the start is {newListStartEndWorking[0]} and the end is {newListStartEndWorking[1]}");
                for (int i = 0; i < deviceDictionary[nameOfDevice].Count; i++)
                {
                    if (listStartEndWorking[0] < deviceDictionary[nameOfDevice][0][0])
                    {
                        newListStartEndWorking[0] = deviceDictionary[nameOfDevice][0][0];
                    }
                    else if (deviceDictionary[nameOfDevice][i][0] <= listStartEndWorking[0] && listStartEndWorking[0] < deviceDictionary[nameOfDevice][i + 1][0])
                    {
                        newListStartEndWorking[0] = deviceDictionary[nameOfDevice][i + 1][0];
                    }
                }

                //Console.WriteLine($"Through the sequence {listReturn[1]}");
                //int limitNumberWorkOnTechnician = maintenanceTechnicianWorkTime[listReturn[1].ToString()].Count;
                //Console.WriteLine($"Number work in sequence {listReturn[1]}: {limitNumberWorkOnTechnician}");
                //foreach(List<DateTime> listTime in maintenanceTechnicianWorkTime[listReturn[1].ToString()])
                //{
                //    Console.WriteLine($"{listTime[0]} - {listTime[1]}");
                //}
                //if (limitNumberWorkOnTechnician < 3)
                //{
                //    newListStartEndWorking[0] = maintenanceTechnicianWorkTime[listReturn[1].ToString()][limitNumberWorkOnTechnician - 1][1].Add(TimeSpan.FromMinutes(1));
                //}
                //else
                //{
                //    foreach (string no in technicianDictionary.Keys)
                //    {
                //        if (no == listReturn[1].ToString())
                //        {
                //            Console.WriteLine($"Go through the specific technician {no}");
                //            continue;
                //        }
                //        if (listStartEndWorking[0] <= technicianDictionary[no][0][0])
                //        {
                //            Console.WriteLine($"Assign the start date as the start of working time technician {no}");
                //            int numberOfWork = technicianDictionary[no].Count;
                //            if (numberOfWork == 0)
                //            {
                //                newListStartEndWorking[0] = technicianDictionary[no][0][0];
                //            }
                //            else
                //            {
                //                newListStartEndWorking[0] = technicianDictionary[no][numberOfWork - 1][0];
                //            }
                //            listReturn[1] = int.Parse(no);
                //            break;
                //        }
                //        else
                //        {
                //            for (int i = 0; i < (technicianDictionary[no].Count - 1); i++)
                //            {
                //                if (technicianDictionary[no][i][0] <= listStartEndWorking[0] && listStartEndWorking[0] < technicianDictionary[no][i + 1][0])
                //                {
                //                    newListStartEndWorking[0] = technicianDictionary[no][i + 1][0];
                //                    listReturn[1] = int.Parse(no);
                //                    break;
                //                }
                //            }
                //        }
                //    }
                //}


                double minutes = double.Parse((string)workTable.Rows[job - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                newListStartEndWorking[1] = newListStartEndWorking[0].Add(executionTime);
                Console.WriteLine($"After change fail {listReturn[0]}, this job {job} have the start is {newListStartEndWorking[0]} and the end is {newListStartEndWorking[1]}");
            }

            List<int> listTemp = checkTimeAvailable(nameOfDevice, listReturn, newListStartEndWorking, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
            if (listTemp[0] == 0)
            {
                Console.WriteLine("CHECK");
                return newListStartEndWorking;
            }
            else if (listTemp[0] != 0)
            {
                newListStartEndWorking = changePlannedDate(workTable, job, nameOfDevice, newListStartEndWorking, listTemp, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
                return newListStartEndWorking;
            }

            Console.WriteLine($"The start and end date after planning are {newListStartEndWorking[0]} - {newListStartEndWorking[1]}");
            return newListStartEndWorking;
        }



        public static List<DateTime> findPlannedDate(DataTable workTable, List<int> soluton, int job, Dictionary<string, List<List<DateTime>>> deviceDictionary, Dictionary<string, List<List<DateTime>>> technicianDictionary, Dictionary<string, List<List<DateTime>>> maintenanceDeviceBreakTime, Dictionary<string, List<List<DateTime>>> maintenanceTechnicianWorkTime)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            List<DateTime> listStartEndWorking = new List<DateTime> { startDate, endDate };
            string nameOfDevice = workTable.Rows[job - 1]["Device"].ToString();
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine($"Name of device: {nameOfDevice}. And this is job {job}");
            int numberWorkOnDevice = maintenanceDeviceBreakTime[nameOfDevice].Count;
            Console.WriteLine($"Number of Work on device: {numberWorkOnDevice}");

            if (numberWorkOnDevice == 0)
            {
                listStartEndWorking[0] = deviceDictionary[nameOfDevice][0][0];
                double minutes = double.Parse((string)workTable.Rows[job - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                listStartEndWorking[1] = listStartEndWorking[0].Add(executionTime);
            }
            else
            {
                listStartEndWorking[0] = maintenanceDeviceBreakTime[nameOfDevice][numberWorkOnDevice - 1][1];
                double minutes = double.Parse((string)workTable.Rows[job - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                listStartEndWorking[1] = listStartEndWorking[0].Add(executionTime);
            }

            List<int> listReturn = new List<int> { 0, 0 };
            listReturn = checkTimeAvailable(nameOfDevice, listReturn, listStartEndWorking, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
            Console.WriteLine($"The number of fail: {listReturn[0]}");
            Console.WriteLine($"The initial sequence of technician: {listReturn[1]}");

            if (listReturn[0] == 0)
            {
                listStartEndWorking = listStartEndWorking;

                List<List<DateTime>> listListDeviceBreakingTime = maintenanceDeviceBreakTime[nameOfDevice];
                List<List<DateTime>> listListTempDevice = new List<List<DateTime>>();
                foreach(List<DateTime> listTemp in listListDeviceBreakingTime)
                {
                    listListTempDevice.Add(listTemp);
                }
                listListTempDevice.Add(listStartEndWorking);
                maintenanceDeviceBreakTime[nameOfDevice] = new List<List<DateTime>>();
                maintenanceDeviceBreakTime[nameOfDevice] = listListTempDevice;

                List<List<DateTime>> listListTechnicianWorkingTime = maintenanceTechnicianWorkTime[listReturn[1].ToString()];
                List<List<DateTime>> listListTempTechnician = new List<List<DateTime>>();
                foreach (List<DateTime> listTemp in listListTechnicianWorkingTime)
                {
                    listListTempTechnician.Add(listTemp);
                }
                listListTempTechnician.Add(listStartEndWorking);
                maintenanceTechnicianWorkTime[listReturn[1].ToString()] = new List<List<DateTime>>();
                maintenanceTechnicianWorkTime[listReturn[1].ToString()] = listListTempTechnician;
            }
            else
            {
                listStartEndWorking = changePlannedDate(workTable, job, nameOfDevice, listStartEndWorking, listReturn, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
                listReturn = checkTimeAvailable(nameOfDevice, listReturn, listStartEndWorking, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
                List<List<DateTime>> listListDeviceBreakingTime = maintenanceDeviceBreakTime[nameOfDevice];
                List<List<DateTime>> listListTempDevice = new List<List<DateTime>>();
                foreach (List<DateTime> listTemp in listListDeviceBreakingTime)
                {
                    listListTempDevice.Add(listTemp);
                }
                listListTempDevice.Add(listStartEndWorking);
                maintenanceDeviceBreakTime[nameOfDevice] = new List<List<DateTime>>();
                maintenanceDeviceBreakTime[nameOfDevice] = listListTempDevice;

                List<List<DateTime>> listListTechnicianWorkingTime = maintenanceTechnicianWorkTime[listReturn[1].ToString()];
                List<List<DateTime>> listListTempTechnician = new List<List<DateTime>>();
                foreach (List<DateTime> listTemp in listListTechnicianWorkingTime)
                {
                    listListTempTechnician.Add(listTemp);
                }
                listListTempTechnician.Add(listStartEndWorking);
                maintenanceTechnicianWorkTime[listReturn[1].ToString()] = new List<List<DateTime>>();
                maintenanceTechnicianWorkTime[listReturn[1].ToString()] = listListTempTechnician;
            }


            Console.WriteLine();
            foreach(string key in maintenanceDeviceBreakTime.Keys)
            {
                Console.WriteLine($"The name of device is checked: {key}");
                foreach(List<DateTime> listTime in maintenanceDeviceBreakTime[key])
                {
                    Console.WriteLine(listTime[0].ToString() + " " + listTime[1].ToString());
                }
            }

            Console.WriteLine();
            foreach (string key in maintenanceTechnicianWorkTime.Keys)
            {
                Console.WriteLine($"The sequence of technician is checked: {key}");
                foreach (List<DateTime> listTime in maintenanceTechnicianWorkTime[key])
                {
                    Console.WriteLine(listTime[0].ToString() + " " + listTime[1].ToString());
                }
            }
            Console.WriteLine();

            return listStartEndWorking;
        }
    }
}
