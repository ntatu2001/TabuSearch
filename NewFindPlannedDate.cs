using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearchProject
{
    public class JobInfor
    {
        public int Id { get; set; }
        public string Device { get; set; }
        public int Technician { get; set; }
        public DateTime startPlannedDate { get; set; }
        public DateTime endPlannedDate { get; set; }
        public int[] arrayFail { get; set; }

        public JobInfor(int id, string device, int technician, DateTime startDate, DateTime endDate) 
        {
            this.Id = id;
            this.Device = device;
            this.Technician = technician;
            this.startPlannedDate = startDate;
            this.endPlannedDate = endDate;
            this.arrayFail = new int[6];
        }
    }

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
        public static JobInfor checkTimeAvailable(JobInfor workInfor,Dictionary<string, List<List<DateTime>>> deviceDictionary, Dictionary<string, List<List<DateTime>>> technicianDictionary, Dictionary<string, List<List<DateTime>>> maintenanceDeviceBreakTime, Dictionary<string, List<List<DateTime>>> maintenanceTechnicianWorkTime)
        {
            bool checkDeviceAvailable = false;
            foreach (List<DateTime> listBreakTime in deviceDictionary[workInfor.Device])
            {
                if (isInRange(listBreakTime[0], listBreakTime[1], workInfor.startPlannedDate) && isInRange(listBreakTime[0], listBreakTime[1], workInfor.endPlannedDate))
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
                    if (isInRange(listWorkTime[0], listWorkTime[1], workInfor.startPlannedDate) && isInRange(listWorkTime[0], listWorkTime[1], workInfor.endPlannedDate))
                    {
                        // Check the available of technician's work time
                        checkTechnicianAvailable = true;
                        break;
                    }
                }

                if (checkTechnicianAvailable && workInfor.Technician == 0)
                {
                    // In addtion, assign the sequence of technician perform this job into second element in listReturn
                    workInfor.Technician = int.Parse(no);
                    break;
                }
            }

            if (checkDeviceAvailable && checkTechnicianAvailable)
            {
                bool checkTechnicianConcur = false;
                foreach (List<DateTime> listTechnicianWorkTime in maintenanceTechnicianWorkTime[workInfor.Technician.ToString()])
                {
                    if (isInRange(listTechnicianWorkTime[0], listTechnicianWorkTime[1], workInfor.startPlannedDate) || isInRange(listTechnicianWorkTime[0], listTechnicianWorkTime[1], workInfor.endPlannedDate))
                    {
                        // Device's break time and Technician's work time are available. 
                        // However, a technician is assigned 2 jobs at the same time
                        checkTechnicianConcur = true;
                        break;
                    }
                }

                if (checkTechnicianConcur == true)
                {
                    workInfor.arrayFail[4] = 1;
                }

                bool checkDeviceConcur = false;
                foreach (List<DateTime> listDeviceBreakTime in maintenanceDeviceBreakTime[workInfor.Device])
                {
                    if (isInRange(listDeviceBreakTime[0], listDeviceBreakTime[1], workInfor.startPlannedDate) || isInRange(listDeviceBreakTime[0], listDeviceBreakTime[1], workInfor.endPlannedDate))
                    {
                        // Device's break time and Technician's work time are available. 
                        // However, there are 2 jobs performing on the same device simultaneously
                        checkDeviceConcur = true;
                        break;
                    }
                }

                if (checkDeviceConcur == true)
                {
                    workInfor.arrayFail[3] = 1;
                }

                if (checkDeviceConcur == false && checkTechnicianConcur == false)
                {
                    // If everything is ok
                    workInfor.arrayFail[0] = 1;
                }
            }
            else if (checkDeviceAvailable == false)
            {
                // If the break time of device is not available, return 1
                workInfor.arrayFail[1] = 1;
            }
            else if (checkTechnicianAvailable == false)
            {
                // If the work time of technician is not available, return 2
                workInfor.arrayFail[2] = 1;
            }

            return workInfor;
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
        public static JobInfor changePlannedDate(DataTable workTable, JobInfor workInfor, Dictionary<string, List<List<DateTime>>> deviceDictionary, Dictionary<string, List<List<DateTime>>> technicianDictionary, Dictionary<string, List<List<DateTime>>> maintenanceDeviceBreakTime, Dictionary<string, List<List<DateTime>>> maintenanceTechnicianWorkTime)
        {
            //Console.WriteLine($"The start planned date have to modified: {workInfor.startPlannedDate}");
            if (workInfor.arrayFail[1] == 1)
            {
                // If the break time of device is not available
                if (maintenanceDeviceBreakTime[workInfor.Device].Count == 0)
                {
                    // This is the first job on this device, assign the planned start date as the first break time of device in a week
                    workInfor.startPlannedDate = deviceDictionary[workInfor.Device][0][0];
                }
                else
                {
                    // This is the rest of job
                    for (int i = 0; i < (deviceDictionary[workInfor.Device].Count - 1); i++)
                    {
                        if (workInfor.startPlannedDate < deviceDictionary[workInfor.Device][0][0])
                        {
                            // If the start calculated is earlier than the first break time of device, assign it as the first break time of device
                            workInfor.startPlannedDate = deviceDictionary[workInfor.Device][0][0];
                        }
                        else if (deviceDictionary[workInfor.Device][i][0] <= workInfor.startPlannedDate && workInfor.startPlannedDate <= deviceDictionary[workInfor.Device][i + 1][0])
                        {
                            // Find the range of start device's break time and assign the planned start as the next start of break time
                            workInfor.startPlannedDate = deviceDictionary[workInfor.Device][i + 1][0];
                        }
                    }
                }

                double minutes = double.Parse((string)workTable.Rows[workInfor.Id - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                workInfor.endPlannedDate = workInfor.startPlannedDate.Add(executionTime);
            }
            
            if (workInfor.arrayFail[2] == 1)
            {
                // If the work time of technician is not available
                foreach (string no in technicianDictionary.Keys)
                {
                    if (workInfor.startPlannedDate <= technicianDictionary[no][0][0])
                    {
                        // If the start calculated is earlier than the first work time of specific technician, assign it as the first work time of specific technician
                        workInfor.startPlannedDate = technicianDictionary[no][0][0];
                        workInfor.Technician = int.Parse(no);
                        break;
                    }
                    else
                    {
                        for (int i = 0; i < (technicianDictionary[no].Count - 1); i++)
                        {
                            if (technicianDictionary[no][i][0] <= workInfor.startPlannedDate && workInfor.startPlannedDate < technicianDictionary[no][i + 1][0])
                            {
                                // Find the range of start technician's work time and assign the planned start as the next start of work time
                                workInfor.startPlannedDate = technicianDictionary[no][i + 1][0];
                                workInfor.Technician = int.Parse(no);
                                break;
                            }
                        }
                    }
                }

                double minutes = double.Parse((string)workTable.Rows[workInfor.Id - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                workInfor.endPlannedDate = workInfor.startPlannedDate.Add(executionTime);
            }
            
            if (workInfor.arrayFail[3] == 1)
            {
                // There are 2 jobs performing on the same device simultaneously. 
                // Assign the planned start as the previous planned end on this device
                int numberOfWorkOnDevice = maintenanceDeviceBreakTime[workInfor.Device].Count;
                workInfor.startPlannedDate = maintenanceDeviceBreakTime[workInfor.Device][numberOfWorkOnDevice - 1][1].Add(TimeSpan.FromMinutes(1));
                double minutes = double.Parse((string)workTable.Rows[workInfor.Id - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                workInfor.endPlannedDate = workInfor.startPlannedDate.Add(executionTime);
            }

            if (workInfor.arrayFail[4] == 1)
            {
                //A technician is assigned 2 jobs at the same time
                for (int i = 0; i < deviceDictionary[workInfor.Device].Count; i++)
                {
                    if (workInfor.startPlannedDate < deviceDictionary[workInfor.Device][0][0])
                    {
                        workInfor.startPlannedDate = deviceDictionary[workInfor.Device][0][0];
                    }
                    if (deviceDictionary[workInfor.Device][i][0] <= workInfor.startPlannedDate && workInfor.startPlannedDate < deviceDictionary[workInfor.Device][i + 1][0])
                    {
                        // Find the range of start device's break time and assign the planned start as the next start of break time
                        workInfor.startPlannedDate = deviceDictionary[workInfor.Device][i + 1][0];
                        break;
                    }
                }

                //Console.WriteLine("Through");
                //List<string> listNoTechnician = technicianDictionary.Keys.ToList();
                //List<string> newListNoTechnician = shuffleList(listNoTechnician);
                //bool check = false;
                //foreach (string no in newListNoTechnician)
                //{
                //    int numberWorkOnTechnician = maintenanceTechnicianWorkTime[no].Count;
                //    Console.WriteLine($"Number of work on technician {no}: {numberWorkOnTechnician}");

                //    if (no == workInfor.Technician.ToString())
                //    {
                //        continue;
                //    }

                //    if (numberWorkOnTechnician == 0)
                //    {
                //        workInfor.startPlannedDate = technicianDictionary[no][0][0];
                //        break;
                //    }

                //    if (workInfor.startPlannedDate <= technicianDictionary[no][0][0])
                //    {
                //        Console.WriteLine($"The sequence of technician with the first: {no}");
                //        Console.WriteLine($"The start planned date 1 before modified: {workInfor.startPlannedDate}");
                //        workInfor.startPlannedDate = technicianDictionary[no][0][0];
                //        Console.WriteLine($"The start planned date 1 after modified: {workInfor.startPlannedDate}");
                //        workInfor.Technician = int.Parse(no);
                //        check = true;
                //    }
                //    else
                //    {
                //        bool check1 = false;
                //        for (int i = 0; i < (technicianDictionary[no].Count - 1); i++)
                //        {
                //            Console.WriteLine($"The start planned date 2 before modified: {workInfor.startPlannedDate}");
                //            Console.WriteLine($"The start work time of technician: {technicianDictionary[no][i][0]}");
                //            if (technicianDictionary[no][i][0] <= workInfor.startPlannedDate && workInfor.startPlannedDate < technicianDictionary[no][i + 1][0])
                //            {
                //                workInfor.startPlannedDate = technicianDictionary[no][i + 1][0];
                //                Console.WriteLine($"The start planned date 2 after modified: {workInfor.startPlannedDate}");
                //                workInfor.Technician = int.Parse(no);
                //                check = true;
                //                check1 = true;
                //            }

                //            if (check1 == true)
                //            {
                //                break;
                //            }
                //        }

                //        //Console.WriteLine($"The start planned date before modified: {workInfor.startPlannedDate}");
                //        //if (numberWorkOnTechnician == 0)
                //        //{
                //        //    workInfor.startPlannedDate = technicianDictionary[no][0][0];
                //        //    check = true;
                //        //}
                //        //else
                //        //{
                //        //    workInfor.startPlannedDate = maintenanceTechnicianWorkTime[no][numberWorkOnTechnician - 1][1].Add(TimeSpan.FromMinutes(1));
                //        //    check = true;
                //        //}
                //    }

                //    if (check == true)
                //    {
                //        break;
                //    }
                //}

                double minutes = double.Parse((string)workTable.Rows[workInfor.Id - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                workInfor.endPlannedDate = workInfor.startPlannedDate.Add(executionTime);
            }


            // After modification, the planned date needs to double-check by checkTimeAvailable.
            workInfor = checkTimeAvailable(workInfor, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
            if (workInfor.arrayFail[0] == 1)
            {
                return workInfor;
            }
            else if (workInfor.arrayFail[0] != 1)
            {
                // If the modified date still have an error, we must modify it again.
                // This will be a loop recall changePlannedDate and checkTimeAvailable methods until this is suitable for implement
                workInfor = changePlannedDate(workTable, workInfor, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
                return workInfor;
            }

            return workInfor;
        }



        public static JobInfor findPlannedDate(DataTable workTable, List<int> soluton, int job, JobInfor workInfor, Dictionary<string, List<List<DateTime>>> deviceDictionary, Dictionary<string, List<List<DateTime>>> technicianDictionary, Dictionary<string, List<List<DateTime>>> maintenanceDeviceBreakTime, Dictionary<string, List<List<DateTime>>> maintenanceTechnicianWorkTime)
        {
            
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;

            string nameOfDevice = workTable.Rows[job - 1]["Device"].ToString();
            //Console.WriteLine("--------------------------------------------------------------");
            //Console.WriteLine($"Name of device: {nameOfDevice}. And this is job {job}");
            int numberWorkOnDevice = maintenanceDeviceBreakTime[nameOfDevice].Count;
            //Console.WriteLine($"Number of Work on device: {numberWorkOnDevice}");

            workInfor = new JobInfor(job, nameOfDevice, 0, startDate, endDate);

            if (numberWorkOnDevice == 0)
            {
                // This is the first job on device
                workInfor.startPlannedDate = deviceDictionary[workInfor.Device][0][0];
                double minutes = double.Parse((string)workTable.Rows[job - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                workInfor.endPlannedDate = workInfor.startPlannedDate.Add(executionTime);
            }
            else
            {
                // Assign the planned start as the previous planned end on this device
                workInfor.startPlannedDate = maintenanceDeviceBreakTime[workInfor.Device][numberWorkOnDevice - 1][1].Add(TimeSpan.FromMinutes(1));
                double minutes = double.Parse((string)workTable.Rows[job - 1]["ExecutionTime"]);
                TimeSpan executionTime = TimeSpan.FromMinutes(minutes);
                workInfor.endPlannedDate = workInfor.startPlannedDate.Add(executionTime);
            }

            // Check: Is the planned date available?
            workInfor = checkTimeAvailable(workInfor, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
            //Console.WriteLine($"The Fail's number: {workInfor.arrayFail[0]} - {workInfor.arrayFail[1]} - {workInfor.arrayFail[2]} - {workInfor.arrayFail[3]} - {workInfor.arrayFail[4]}");

            if (workInfor.arrayFail[0] == 1)
            {
                List<DateTime> listStartEndWorking = new List<DateTime> { workInfor.startPlannedDate, workInfor.endPlannedDate };
                // Add the record into maintenanceDeviceBreakTime
                List<List<DateTime>> listListDeviceBreakingTime = maintenanceDeviceBreakTime[workInfor.Device];
                List<List<DateTime>> listListTempDevice = new List<List<DateTime>>();
                foreach (List<DateTime> listTemp in listListDeviceBreakingTime)
                {
                    listListTempDevice.Add(listTemp);
                }
                listListTempDevice.Add(listStartEndWorking);
                maintenanceDeviceBreakTime[workInfor.Device] = new List<List<DateTime>>();
                maintenanceDeviceBreakTime[workInfor.Device] = listListTempDevice;

                // Add the record into maintenanceTechnicianWorkTime
                List<List<DateTime>> listListTechnicianWorkingTime = maintenanceTechnicianWorkTime[workInfor.Technician.ToString()];
                List<List<DateTime>> listListTempTechnician = new List<List<DateTime>>();
                foreach (List<DateTime> listTemp in listListTechnicianWorkingTime)
                {
                    listListTempTechnician.Add(listTemp);
                }
                listListTempTechnician.Add(listStartEndWorking);
                maintenanceTechnicianWorkTime[workInfor.Technician.ToString()] = new List<List<DateTime>>();
                maintenanceTechnicianWorkTime[workInfor.Technician.ToString()] = listListTempTechnician;
            }
            else
            {
                // Get the modified date by changePlannedDate
                workInfor = changePlannedDate(workTable, workInfor, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);
                // Get the sequence of Technician perform this job
                workInfor = checkTimeAvailable(workInfor, deviceDictionary, technicianDictionary, maintenanceDeviceBreakTime, maintenanceTechnicianWorkTime);

                List<DateTime> listStartEndWorking = new List<DateTime> { workInfor.startPlannedDate, workInfor.endPlannedDate };
                // Add the record into maintenanceDeviceBreakTime
                List<List<DateTime>> listListDeviceBreakingTime = maintenanceDeviceBreakTime[workInfor.Device];
                List<List<DateTime>> listListTempDevice = new List<List<DateTime>>();
                foreach (List<DateTime> listTemp in listListDeviceBreakingTime)
                {
                    listListTempDevice.Add(listTemp);
                }
                listListTempDevice.Add(listStartEndWorking);
                maintenanceDeviceBreakTime[workInfor.Device] = new List<List<DateTime>>();
                maintenanceDeviceBreakTime[workInfor.Device] = listListTempDevice;

                // Add the record into maintenanceTechnicianWorkTime
                List<List<DateTime>> listListTechnicianWorkingTime = maintenanceTechnicianWorkTime[workInfor.Technician.ToString()];
                List<List<DateTime>> listListTempTechnician = new List<List<DateTime>>();
                foreach (List<DateTime> listTemp in listListTechnicianWorkingTime)
                {
                    listListTempTechnician.Add(listTemp);
                }
                listListTempTechnician.Add(listStartEndWorking);
                maintenanceTechnicianWorkTime[workInfor.Technician.ToString()] = new List<List<DateTime>>();
                maintenanceTechnicianWorkTime[workInfor.Technician.ToString()] = listListTempTechnician;
            }

            //Console.WriteLine();
            //Console.WriteLine($"The start and end planned date are: {workInfor.startPlannedDate} - {workInfor.endPlannedDate}");
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

            return workInfor;
        }
    }
}
