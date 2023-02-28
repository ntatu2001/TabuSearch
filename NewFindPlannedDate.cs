using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearchProject
{
    public class NewFindPlannedDate
    {
        /// <summary>
        /// Create a dictionary containing the break time of each device in a week
        /// key is the name of device and move value is List of list DateTime include List {From, To}
        /// </summary>
        /// <param name="deviceTable"></param>
        /// <returns></returns>
        public static Dictionary<string, List<List<DateTime>>> getDeviceDictionary(DataTable deviceTable)
        {
            Dictionary<string, List<List<DateTime>>> deviceDictionary = new Dictionary<string, List<List<DateTime>>>();
            List<string> listDevice = new List<string>();
            List<DateTime> listDateTimeTemp = new List<DateTime>();
            int j = 0;
            // We create a listDevice contains all of device's name and listDateTimeTemp contains all of break time of all devices.
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

            // We divide listDateTimeTemp into listListTimeDevice which add each pair of {From, To}
            List<List<DateTime>> listListTimeDevice = new List<List<DateTime>>();
            for (int i = 0; i < 24 * listDevice.Count; i++)
            {
                if (i % 2 == 0)
                {
                    List<DateTime> listTemp = new List<DateTime> { listDateTimeTemp[i], listDateTimeTemp[i + 1] };
                    listListTimeDevice.Add(listTemp);
                }
            }

            // Create a dictionary
            for (int m = 0; m < listDevice.Count; m++)
            {
                List<List<DateTime>> listListTemp = new List<List<DateTime>>();
                for (int index = 12 * m; index < 12 * (m + 1); index++)
                {
                    listListTemp.Add(listListTimeDevice[index]);
                }

                deviceDictionary.Add(listDevice[m], listListTemp);
            }


            // Print all of dictionary
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


        /// <summary>
        /// Create a dictionary containing the work time of each technician in a week
        /// key is the sequence of technician and move value is List of list DateTime include List {From, To}
        /// </summary>
        /// <param name="technicianTable"></param>
        /// <returns></returns>
        public static Dictionary<string, List<List<DateTime>>> getTechnicianDictionary(DataTable technicianTable)
        {
            Dictionary<string, List<List<DateTime>>> technicianDictionary = new Dictionary<string, List<List<DateTime>>>();
            List<string> listTechnician = new List<string>();
            List<DateTime> listDateTimeTemp = new List<DateTime>();
            int j = 0;
            // We create a listTechnician contains all of technician's sequence and listDateTimeTemp contains all of work time of all technicians.
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

            // We divide listDateTimeTemp into listListTimeDevice which add each pair of {From, To}
            List<List<DateTime>> listListTimeDevice = new List<List<DateTime>>();
            for (int i = 0; i < 24 * listTechnician.Count; i++)
            {
                if (i % 2 == 0)
                {
                    List<DateTime> listTemp = new List<DateTime> { listDateTimeTemp[i], listDateTimeTemp[i + 1] };
                    listListTimeDevice.Add(listTemp);
                }
            }

            // Create a dictionary
            for (int m = 0; m < listTechnician.Count; m++)
            {
                List<List<DateTime>> listListTemp = new List<List<DateTime>>();
                for (int index = 12 * m; index < 12 * (m + 1); index++)
                {
                    listListTemp.Add(listListTimeDevice[index]);
                }

                technicianDictionary.Add(listTechnician[m], listListTemp);
            }

            // Print all of dictionary
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

        /// <summary>
        /// Check: Is the considered date exist in range of (startDate, endDate)?
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="checkDate"></param>
        /// <returns></returns>
        public static bool isInRange(DateTime startDate, DateTime endDate, DateTime checkDate)
        {
            return (startDate <= checkDate) && (checkDate <= endDate);
        }

        /// <summary>
        /// Create an empty dictionary is similar to deviceDictionary. 
        /// </summary>
        /// <param name="deviceDictionary"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Create an empty dictionary is similar to technicianDictionary. 
        /// </summary>
        /// <param name="technicianDictionary"></param>
        /// <returns></returns>
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


        public static List<string> shuffleList(List<string> list)
        {
            var random = new Random();
            var newShuffledList = new List<string>();
            var listcCount = list.Count;
            for (int i = 0; i < listcCount; i++)
            {
                var randomElementInList = random.Next(0, list.Count);
                newShuffledList.Add(list[randomElementInList]);
                list.Remove(list[randomElementInList]);
            }
            return newShuffledList;
        }

        /// <summary>
        /// When we have already calculated the planned date, this method to check the available of this date and the seuquence of technician assigned
        /// If everything is ok, return 0
        /// If the break time of device is not available, return 1
        /// If the work time of technician is not available, return 2
        /// If there are 2 jobs performing on the same device simultaneously, return 3
        /// If a technician is assigned 2 jobs at the same time, return 4
        /// </summary>
        /// <param name="nameOfDevice"></param>
        /// <param name="listReturn"></param>
        /// <param name="listStartEndWorking"></param>
        /// <param name="deviceDictionary"></param>
        /// <param name="technicianDictionary"></param>
        /// <param name="maintenanceDeviceBreakTime"></param>
        /// <param name="maintenanceTechnicianWorkTime"></param>
        /// <returns></returns>
        public static int[] checkTimeAvailable(string nameOfDevice, int[] arrayReturn, List<DateTime> listStartEndWorking, Dictionary<string, List<List<DateTime>>> deviceDictionary, Dictionary<string, List<List<DateTime>>> technicianDictionary, Dictionary<string, List<List<DateTime>>> maintenanceDeviceBreakTime, Dictionary<string, List<List<DateTime>>> maintenanceTechnicianWorkTime)
        {
            bool checkDeviceAvailable = false;
            foreach (List<DateTime> listBreakTime in deviceDictionary[nameOfDevice])
            {
                if (isInRange(listBreakTime[0], listBreakTime[1], listStartEndWorking[0]) && isInRange(listBreakTime[0], listBreakTime[1], listStartEndWorking[1]))
                {
                    //  Check the available of device's break time
                    checkDeviceAvailable = true;
                    break;
                }
            }

            bool checkTechnicianAvailable = false;
            List<string> listNoTechnician = technicianDictionary.Keys.ToList();
            List<string> newListNoTechnician = shuffleList(listNoTechnician);
            foreach (string no in newListNoTechnician)
            {
                foreach (List<DateTime> listWorkTime in technicianDictionary[no])
                {
                    if (isInRange(listWorkTime[0], listWorkTime[1], listStartEndWorking[0]) && isInRange(listWorkTime[0], listWorkTime[1], listStartEndWorking[1]))
                    {
                        // Check the available of technician's work time
                        checkTechnicianAvailable = true;
                        break;
                    }
                }

                if (checkTechnicianAvailable && arrayReturn[5] == 0)
                {
                    // In addtion, assign the sequence of technician perform this job into second element in listReturn
                    arrayReturn[5] = int.Parse(no);
                    break;
                }
            }

            if (checkDeviceAvailable && checkTechnicianAvailable)
            {
                bool checkTechnicianConcur = false;
                foreach (List<DateTime> listTechnicianWorkTime in maintenanceTechnicianWorkTime[arrayReturn[5].ToString()])
                {
                    if (isInRange(listTechnicianWorkTime[0], listTechnicianWorkTime[1], listStartEndWorking[0]) || isInRange(listTechnicianWorkTime[0], listTechnicianWorkTime[1], listStartEndWorking[1]))
                    {
                        // Device's break time and Technician's work time are available. 
                        // However, a technician is assigned 2 jobs at the same time
                        checkTechnicianConcur = true;
                        break;
                    }
                }

                if (checkTechnicianConcur == true)
                {
                    arrayReturn[4] = 1;
                }

                bool checkDeviceConcur = false;
                foreach (List<DateTime> listDeviceBreakTime in maintenanceDeviceBreakTime[nameOfDevice])
                {
                    if (isInRange(listDeviceBreakTime[0], listDeviceBreakTime[1], listStartEndWorking[0]) || isInRange(listDeviceBreakTime[0], listDeviceBreakTime[1], listStartEndWorking[1]))
                    {
                        // Device's break time and Technician's work time are available. 
                        // However, there are 2 jobs performing on the same device simultaneously
                        checkDeviceConcur = true;
                        break;
                    }
                }

                if (checkDeviceConcur == true)
                {
                    arrayReturn[3] = 1;
                }

                if (checkDeviceConcur == false && checkTechnicianConcur == false)
                {
                    // If everything is ok
                    arrayReturn[0] = 1;
                }
            }
            else if (checkDeviceAvailable == false)
            {
                // If the break time of device is not available, return 1
                arrayReturn[1] = 1;
            }
            else if (checkTechnicianAvailable == false)
            {
                // If the work time of technician is not available, return 2
                arrayReturn[2] = 1;
            }

            return arrayReturn;
        }

        /// <summary>
        /// If the planned date have an error, this method will modify the planned date and double-check until this date is suitable for implement
        /// </summary>
        /// <param name="workTable"></param>
        /// <param name="job"></param>
        /// <param name="nameOfDevice"></param>
        /// <param name="listStartEndWorking"></param>
        /// <param name="listReturn"></param>
        /// <param name="deviceDictionary"></param>
        /// <param name="technicianDictionary"></param>
        /// <param name="maintenanceDeviceBreakTime"></param>
        /// <param name="maintenanceTechnicianWorkTime"></param>
        /// <returns></returns>
        public static List<DateTime> changePlannedDate(DataTable workTable, int job, string nameOfDevice, List<DateTime> listStartEndWorking, int[] arrayReturn, Dictionary<string, List<List<DateTime>>> deviceDictionary, Dictionary<string, List<List<DateTime>>> technicianDictionary, Dictionary<string, List<List<DateTime>>> maintenanceDeviceBreakTime, Dictionary<string, List<List<DateTime>>> maintenanceTechnicianWorkTime)
        {
            List<DateTime> newListStartEndWorking = new List<DateTime> { listStartEndWorking[0], listStartEndWorking[1] };

            if (arrayReturn[1] == 1)
            {
                // If the break time of device is not available
                if (maintenanceDeviceBreakTime[nameOfDevice].Count == 0)
                {
                    // This is the first job on this device, assign the planned start date as the first break time of device in a week
                    newListStartEndWorking[0] = deviceDictionary[nameOfDevice][0][0];
                }
                else
                {
                    // This is the rest of job
                    for (int i = 0; i < (deviceDictionary[nameOfDevice].Count - 1); i++)
                    {
                        if (listStartEndWorking[0] < deviceDictionary[nameOfDevice][0][0])
                        {
                            // If the start calculated is earlier than the first break time of device, assign it as the first break time of device
                            newListStartEndWorking[0] = deviceDictionary[nameOfDevice][0][0];
                        }
                        else if (deviceDictionary[nameOfDevice][i][0] <= listStartEndWorking[0] && listStartEndWorking[0] <= deviceDictionary[nameOfDevice][i + 1][0])
                        {
                            // Find the range of start device's break time and assign the planned start as the next start of break time
                            newListStartEndWorking[0] = deviceDictionary[nameOfDevice][i + 1][0];
                        }
                    }
                }

                double minutes = double.Parse((string)workTable.Rows[job - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                newListStartEndWorking[1] = newListStartEndWorking[0].Add(executionTime);
            }
            
            if (arrayReturn[2] == 1)
            {
                // If the work time of technician is not available
                foreach (string no in technicianDictionary.Keys)
                {
                    if (listStartEndWorking[0] <= technicianDictionary[no][0][0])
                    {
                        // If the start calculated is earlier than the first work time of specific technician, assign it as the first work time of specific technician
                        newListStartEndWorking[0] = technicianDictionary[no][0][0];
                        arrayReturn[5] = int.Parse(no);
                        break;
                    }
                    else
                    {
                        for (int i = 0; i < (technicianDictionary[no].Count - 1); i++)
                        {
                            if (technicianDictionary[no][i][0] <= listStartEndWorking[0] && listStartEndWorking[0] < technicianDictionary[no][i + 1][0])
                            {
                                // Find the range of start technician's work time and assign the planned start as the next start of work time
                                newListStartEndWorking[0] = technicianDictionary[no][i + 1][0];
                                arrayReturn[5] = int.Parse(no);
                                break;
                            }
                        }
                    }
                }

                double minutes = double.Parse((string)workTable.Rows[job - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                newListStartEndWorking[1] = newListStartEndWorking[0].Add(executionTime);
            }
            
            if (arrayReturn[3] == 1)
            {
                // There are 2 jobs performing on the same device simultaneously. 
                // Assign the planned start as the previous planned end on this device
                int numberOfWorkOnDevice = maintenanceDeviceBreakTime[nameOfDevice].Count;
                newListStartEndWorking[0] = maintenanceDeviceBreakTime[nameOfDevice][numberOfWorkOnDevice - 1][1].Add(TimeSpan.FromMinutes(1));
                double minutes = double.Parse((string)workTable.Rows[job - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                newListStartEndWorking[1] = newListStartEndWorking[0].Add(executionTime);
            }
            
            if (arrayReturn[4] == 1)
            {
                // A technician is assigned 2 jobs at the same time
                for (int i = 0; i < deviceDictionary[nameOfDevice].Count; i++)
                {
                    if (listStartEndWorking[0] < deviceDictionary[nameOfDevice][0][0])
                    {
                        newListStartEndWorking[0] = deviceDictionary[nameOfDevice][0][0];
                    }
                    else
                    if (deviceDictionary[nameOfDevice][i][0] <= listStartEndWorking[0] && listStartEndWorking[0] < deviceDictionary[nameOfDevice][i + 1][0])
                    {
                        // Find the range of start device's break time and assign the planned start as the next start of break time
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
            }

            // After modification, the planned date needs to double-check by checkTimeAvailable.
            int[] arrayTemp = checkTimeAvailable(nameOfDevice, arrayReturn, newListStartEndWorking, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
            if (arrayTemp[0] == 1)
            {
                return newListStartEndWorking;
            }
            else if (arrayTemp[0] != 1)
            {
                // If the modified date still have an error, we must modify it again.
                // This will be a loop recall changePlannedDate and checkTimeAvailable methods until this is suitable for implement
                newListStartEndWorking = changePlannedDate(workTable, job, nameOfDevice, newListStartEndWorking, arrayTemp, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
                return newListStartEndWorking;
            }

            return newListStartEndWorking;
        }



        public static List<DateTime> findPlannedDate(DataTable workTable, List<int> soluton, int job, Dictionary<string, List<List<DateTime>>> deviceDictionary, Dictionary<string, List<List<DateTime>>> technicianDictionary, Dictionary<string, List<List<DateTime>>> maintenanceDeviceBreakTime, Dictionary<string, List<List<DateTime>>> maintenanceTechnicianWorkTime)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            // Create a List of DateTime with the start date and end date
            List<DateTime> listStartEndWorking = new List<DateTime> { startDate, endDate };
            string nameOfDevice = workTable.Rows[job - 1]["Device"].ToString();
            //Console.WriteLine("--------------------------------------------------------------");
            //Console.WriteLine($"Name of device: {nameOfDevice}. And this is job {job}");
            int numberWorkOnDevice = maintenanceDeviceBreakTime[nameOfDevice].Count;
            //Console.WriteLine($"Number of Work on device: {numberWorkOnDevice}");

            if (numberWorkOnDevice == 0)
            {
                // This is the first job on device
                listStartEndWorking[0] = deviceDictionary[nameOfDevice][0][0];
                double minutes = double.Parse((string)workTable.Rows[job - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                listStartEndWorking[1] = listStartEndWorking[0].Add(executionTime);
            }
            else
            {
                // Assign the planned start as the previous planned end on this device
                listStartEndWorking[0] = maintenanceDeviceBreakTime[nameOfDevice][numberWorkOnDevice - 1][1];
                double minutes = double.Parse((string)workTable.Rows[job - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                listStartEndWorking[1] = listStartEndWorking[0].Add(executionTime);
            }

            int[] arrayReturn = new int[6];
            // Check: Is the planned date available?
            arrayReturn = checkTimeAvailable(nameOfDevice, arrayReturn, listStartEndWorking, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
            //Console.WriteLine($"The Fail's number: {arrayReturn[0]} - {arrayReturn[1]} - {arrayReturn[2]} - {arrayReturn[3]} - {arrayReturn[4]}");

            if (arrayReturn[0] == 1)
            {
                // There is no error
                listStartEndWorking = listStartEndWorking;

                // Add the record into maintenanceDeviceBreakTime
                List<List<DateTime>> listListDeviceBreakingTime = maintenanceDeviceBreakTime[nameOfDevice];
                List<List<DateTime>> listListTempDevice = new List<List<DateTime>>();
                foreach (List<DateTime> listTemp in listListDeviceBreakingTime)
                {
                    listListTempDevice.Add(listTemp);
                }
                listListTempDevice.Add(listStartEndWorking);
                maintenanceDeviceBreakTime[nameOfDevice] = new List<List<DateTime>>();
                maintenanceDeviceBreakTime[nameOfDevice] = listListTempDevice;

                // Add the record into maintenanceTechnicianWorkTime
                List<List<DateTime>> listListTechnicianWorkingTime = maintenanceTechnicianWorkTime[arrayReturn[5].ToString()];
                List<List<DateTime>> listListTempTechnician = new List<List<DateTime>>();
                foreach (List<DateTime> listTemp in listListTechnicianWorkingTime)
                {
                    listListTempTechnician.Add(listTemp);
                }
                listListTempTechnician.Add(listStartEndWorking);
                maintenanceTechnicianWorkTime[arrayReturn[5].ToString()] = new List<List<DateTime>>();
                maintenanceTechnicianWorkTime[arrayReturn[5].ToString()] = listListTempTechnician;
            }
            else
            {
                // Get the modified date by changePlannedDate
                listStartEndWorking = changePlannedDate(workTable, job, nameOfDevice, listStartEndWorking, arrayReturn, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
                // Get the sequence of Technician perform this job
                arrayReturn = checkTimeAvailable(nameOfDevice, arrayReturn, listStartEndWorking, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);

                // Add the record into maintenanceDeviceBreakTime
                List<List<DateTime>> listListDeviceBreakingTime = maintenanceDeviceBreakTime[nameOfDevice];
                List<List<DateTime>> listListTempDevice = new List<List<DateTime>>();
                foreach (List<DateTime> listTemp in listListDeviceBreakingTime)
                {
                    listListTempDevice.Add(listTemp);
                }
                listListTempDevice.Add(listStartEndWorking);
                maintenanceDeviceBreakTime[nameOfDevice] = new List<List<DateTime>>();
                maintenanceDeviceBreakTime[nameOfDevice] = listListTempDevice;

                // Add the record into maintenanceTechnicianWorkTime
                List<List<DateTime>> listListTechnicianWorkingTime = maintenanceTechnicianWorkTime[arrayReturn[5].ToString()];
                List<List<DateTime>> listListTempTechnician = new List<List<DateTime>>();
                foreach (List<DateTime> listTemp in listListTechnicianWorkingTime)
                {
                    listListTempTechnician.Add(listTemp);
                }
                listListTempTechnician.Add(listStartEndWorking);
                maintenanceTechnicianWorkTime[arrayReturn[5].ToString()] = new List<List<DateTime>>();
                maintenanceTechnicianWorkTime[arrayReturn[5].ToString()] = listListTempTechnician;
            }

            //Console.WriteLine();
            //Console.WriteLine($"The start and end planned date are: {listStartEndWorking[0]} - {listStartEndWorking[1]}");
            //Console.WriteLine();
            //// Print all of maintenance device's record
            //foreach (string key in maintenanceDeviceBreakTime.Keys)
            //{
            //    Console.WriteLine($"The name of device is checked: {key}");
            //    foreach (List<DateTime> listTime in maintenanceDeviceBreakTime[key])
            //    {
            //        Console.WriteLine(listTime[0].ToString() + " " + listTime[1].ToString());
            //    }
            //}

            //Console.WriteLine();
            //// Print all of maintenance technician's record
            //foreach (string key in maintenanceTechnicianWorkTime.Keys)
            //{
            //    Console.WriteLine($"The sequence of technician is checked: {key}");
            //    foreach (List<DateTime> listTime in maintenanceTechnicianWorkTime[key])
            //    {
            //        Console.WriteLine(listTime[0].ToString() + " " + listTime[1].ToString());
            //    }
            //}
            //Console.WriteLine();

            return listStartEndWorking;
        }
    }
}
