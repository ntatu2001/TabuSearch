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
        /// <summary>
        /// Create a dictionary containing the break time of each device in a week
        /// key is the name of device and move value is List of list DateTime include List {From, To}
        /// </summary>
        /// <param name="deviceTable"></param>
        /// <returns></returns>
        public static Dictionary<string, List<List<DateTime>>> getDeviceDictionary (DataTable deviceTable)
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
            for (int i = 0; i < 24*listDevice.Count; i++)
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
                for (int index = 12*m; index < 12*(m + 1); index++)
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
        public static List<int> checkTimeAvailable(string nameOfDevice, List<int> listReturn, List<DateTime> listStartEndWorking, Dictionary<string, List<List<DateTime>>> deviceDictionary, Dictionary<string, List<List<DateTime>>> technicianDictionary, Dictionary<string, List<List<DateTime>>> maintenanceDeviceBreakTime, Dictionary<string, List<List<DateTime>>> maintenanceTechnicianWorkTime)
        {
            bool checkDeviceAvailable = false;
            foreach(List<DateTime> listBreakTime in deviceDictionary[nameOfDevice])
            {
                if (isInRange(listBreakTime[0], listBreakTime[1], listStartEndWorking[0]) && isInRange(listBreakTime[0], listBreakTime[1], listStartEndWorking[1]))
                {
                    //  Check the available of device's break time
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
                        // Check the available of technician's work time
                        checkTechnicianAvailable = true;
                        break;
                    }
                }

                if (checkTechnicianAvailable && listReturn[1] == 0 )
                {
                    // In addtion, assign the sequence of technician perform this job into second element in listReturn
                    listReturn[1] = int.Parse(no);
                    break;
                }
            }

            if (checkDeviceAvailable && checkTechnicianAvailable)
            {
                bool checkTechnicianConcur = false;
                foreach(List<DateTime> listTechnicianWorkTime in maintenanceTechnicianWorkTime[listReturn[1].ToString()])
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
                    listReturn[0] = 4;
                    return listReturn;
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
                    listReturn[0] = 3;
                    return listReturn;
                }

                if (checkDeviceConcur == false && checkTechnicianConcur == false)
                {
                    // If everything is ok
                    listReturn[0] = 0;
                    return listReturn;
                }
            }
            else if (checkDeviceAvailable == false)
            {
                // If the break time of device is not available, return 1
                listReturn[0] = 1;
                return listReturn;
            }
            else if (checkTechnicianAvailable == false)
            {
                // If the work time of technician is not available, return 2
                listReturn[0] = 2;
                return listReturn;
            }

            listReturn[0] = 0;
            return listReturn;
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
        public static List<DateTime> changePlannedDate(DataTable workTable, int job, string nameOfDevice, List<DateTime> listStartEndWorking, List<int> listReturn, Dictionary<string, List<List<DateTime>>> deviceDictionary, Dictionary<string, List<List<DateTime>>> technicianDictionary, Dictionary<string, List<List<DateTime>>> maintenanceDeviceBreakTime, Dictionary<string, List<List<DateTime>>> maintenanceTechnicianWorkTime)
        {
            List<DateTime> newListStartEndWorking = new List<DateTime> { listStartEndWorking[0], listStartEndWorking[1] };

            if (listReturn[0] == 1)
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
            else if (listReturn[0] == 2)
            {
                // If the work time of technician is not available
                foreach (string no in technicianDictionary.Keys)
                {
                    if (listStartEndWorking[0] <= technicianDictionary[no][0][0])
                    {
                        // If the start calculated is earlier than the first work time of specific technician, assign it as the first work time of specific technician
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
                                // Find the range of start technician's work time and assign the planned start as the next start of work time
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
            }
            else if (listReturn[0] == 3)
            {
                // There are 2 jobs performing on the same device simultaneously. 
                // Assign the planned start as the previous planned end on this device
                int numberOfWorkOnDevice = maintenanceDeviceBreakTime[nameOfDevice].Count;
                newListStartEndWorking[0] = maintenanceDeviceBreakTime[nameOfDevice][numberOfWorkOnDevice - 1][1].Add(TimeSpan.FromMinutes(1));
                double minutes = double.Parse((string)workTable.Rows[job - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                newListStartEndWorking[1] = newListStartEndWorking[0].Add(executionTime);
            }
            else if (listReturn[0] == 4)
            {
                // A technician is assigned 2 jobs at the same time
                for (int i = 0; i < deviceDictionary[nameOfDevice].Count; i++)
                {
                    if (listStartEndWorking[0] < deviceDictionary[nameOfDevice][0][0])
                    {
                        newListStartEndWorking[0] = deviceDictionary[nameOfDevice][0][0];
                    }
                    else if (deviceDictionary[nameOfDevice][i][0] <= listStartEndWorking[0] && listStartEndWorking[0] < deviceDictionary[nameOfDevice][i + 1][0])
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
            List<int> listTemp = checkTimeAvailable(nameOfDevice, listReturn, newListStartEndWorking, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
            if (listTemp[0] == 0)
            {
                return newListStartEndWorking;
            }
            else if (listTemp[0] != 0)
            {
                // If the modified date still have an error, we must modify it again.
                // This will be a loop recall changePlannedDate and checkTimeAvailable methods until this is suitable for implement
                newListStartEndWorking = changePlannedDate(workTable, job, nameOfDevice, newListStartEndWorking, listTemp, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
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
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine($"Name of device: {nameOfDevice}. And this is job {job}");
            int numberWorkOnDevice = maintenanceDeviceBreakTime[nameOfDevice].Count;
            Console.WriteLine($"Number of Work on device: {numberWorkOnDevice}");

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

            List<int> listReturn = new List<int> { 0, 0 };
            // Check: Is the planned date available?
            listReturn = checkTimeAvailable(nameOfDevice, listReturn, listStartEndWorking, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
            Console.WriteLine($"The number of fail: {listReturn[0]}");

            if (listReturn[0] == 0)
            {   
                // There is no error
                listStartEndWorking = listStartEndWorking;

                // Add the record into maintenanceDeviceBreakTime
                List<List<DateTime>> listListDeviceBreakingTime = maintenanceDeviceBreakTime[nameOfDevice];
                List<List<DateTime>> listListTempDevice = new List<List<DateTime>>();
                foreach(List<DateTime> listTemp in listListDeviceBreakingTime)
                {
                    listListTempDevice.Add(listTemp);
                }
                listListTempDevice.Add(listStartEndWorking);
                maintenanceDeviceBreakTime[nameOfDevice] = new List<List<DateTime>>();
                maintenanceDeviceBreakTime[nameOfDevice] = listListTempDevice;

                // Add the record into maintenanceTechnicianWorkTime
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
                // Get the modified date by changePlannedDate
                listStartEndWorking = changePlannedDate(workTable, job, nameOfDevice, listStartEndWorking, listReturn, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
                // Get the sequence of Technician perform this job
                listReturn = checkTimeAvailable(nameOfDevice, listReturn, listStartEndWorking, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);

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
            Console.WriteLine($"The start and end planned date are: {listStartEndWorking[0]} - {listStartEndWorking[1]}");
            Console.WriteLine();
            // Print all of maintenance device's record
            foreach(string key in maintenanceDeviceBreakTime.Keys)
            {
                Console.WriteLine($"The name of device is checked: {key}");
                foreach(List<DateTime> listTime in maintenanceDeviceBreakTime[key])
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

            return listStartEndWorking;
        }
    }
}
