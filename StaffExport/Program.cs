using System;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;
using System.Globalization;


namespace StaffExport
{
    class Program
    {
        static void Main(string[] args)
        {
            using (
                var conn = new SqlConnection("Server=0;Database=0;User ID=0;Password= 0;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;")
                )
            {
                conn.Open();
                bool quit = false;
                string choice;
                string staff = "default";
                string wftype = "default";
                string path = "default";
                string wfid = "default";
                SqlCommand cmd = new SqlCommand();
                Console.WriteLine("<---------------------");
                Console.WriteLine("       Staff");
                Console.WriteLine("--------------------->");

                while (!quit)
                {
                    Console.WriteLine("Select by staffType: K12Staff, psStaff, WFStaff or all?");
                    string staffType = Console.ReadLine();
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    staffType = textInfo.ToLower(staffType);
                    staff = staffType;
                    if (staffType == "k12staff")
                    {
                        Console.WriteLine("Select by code, date, both, or none?");
                        choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "code":
                                Console.WriteLine("Select by code1 or code2?");
                                string codeCol = Console.ReadLine();
                                Console.WriteLine("Enter desired code");
                                string code = Console.ReadLine();
                                cmd = new SqlCommand("SELECT * FROM Staff WHERE (Staff." + @codeCol + "='" + @code + "') AND staffType ='K12Staff' FOR XML PATH('staff'), ROOT('k12Staff')", conn);
                                quit = true;
                                break;

                            case "date":
                                Console.WriteLine("Enter desired date using the format MM/DD/YYYY.");
                                string date = Console.ReadLine();
                                cmd = new SqlCommand("SELECT * FROM Staff WHERE (Staff.date_created >= '" + @date + "' OR Staff.date_updated >= '" + @date + "') AND staffType ='K12Staff' FOR XML PATH('staff'), ROOT('k12Staff')", conn);
                                quit = true;
                                break;

                            case "both":
                                cmd = new SqlCommand("SELECT * FROM Staff WHERE staffType ='K12Staff'FOR XML PATH('staff'), ROOT('k12Staff')", conn);
                                quit = true;
                                break;

                            case "none":
                                cmd = new SqlCommand("SELECT * FROM Staff WHERE staffType ='K12Staff' FOR XML PATH('staff'), ROOT('lk12Staff')", conn);
                                quit = true;
                                break;

                            default:
                                Console.WriteLine("Unknown Command " + choice);
                                continue;
                        }
                    }
                    else if (staffType == "psstaff")
                    {
                        Console.WriteLine("Select by code, date, both, or none?");
                        choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "code":
                                Console.WriteLine("Select by code1 or code2?");
                                string codeCol = Console.ReadLine();
                                Console.WriteLine("Enter desired code");
                                string code = Console.ReadLine();
                                cmd = new SqlCommand("SELECT Staff.firstName, Staff.lastName, Staff.positionTitle, Staff.phoneNumber, Staff.email, psInstitution.psi_ipedsID, psInstitution.psi_campus FROM Staff, psInstitution WHERE (Staff." + @codeCol + "='" + @code + "') AND Staff.staffType ='psStaff' AND psInstitution.psi_refid = Staff.psID FOR XML PATH('psStaff'), ROOT('psStaffType')", conn);
                                quit = true;
                                break;

                            case "date":
                                Console.WriteLine("Enter desired date using the format MM/DD/YYYY.");
                                string date = Console.ReadLine();
                                cmd = new SqlCommand("SELECT Staff.firstName, Staff.lastName, Staff.positionTitle, Staff.phoneNumber, Staff.email, psInstitution.psi_ipedsID, psInstitution.psi_campus FROM Staff, psInstitution WHERE (Staff.date_created >= '" + @date + "' OR Staff.date_updated >= '" + @date + "') AND Staff.staffType ='psStaff' AND psInstitution.psi_refid = Staff.psID FOR XML PATH('psStaff'), ROOT('psStaffType')", conn);
                                quit = true;
                                break;

                            case "both":
                                cmd = new SqlCommand("SELECT Staff.firstName, Staff.lastName, Staff.positionTitle, Staff.phoneNumber, Staff.email, psInstitution.psi_ipedsID, psInstitution.psi_campus FROM Staff, psInstitution WHERE Staff.staffType ='psStaff' AND psInstitution.psi_refid = Staff.psID FOR XML PATH('psStaff'), ROOT('psStaffType')", conn);
                                quit = true;
                                break;

                            case "none":
                                cmd = new SqlCommand("SELECT Staff.firstName, Staff.lastName, Staff.positionTitle, Staff.phoneNumber, Staff.email, psInstitution.psi_ipedsID, psInstitution.psi_campus FROM Staff, psInstitution WHERE Staff.staffType ='psStaff' AND psInstitution.psi_refid = Staff.psID FOR XML PATH('psStaff'), ROOT('psStaffType')", conn);
                                quit = true;
                                break;
                               
                            default:
                                Console.WriteLine("Unknown Command " + choice);
                                continue;
                        }
                    }
                    else if (staffType == "wfstaff")
                    {
                        Console.WriteLine("Select by WFRegion or WFCenter?");
                        wftype = Console.ReadLine();
                        TextInfo textInfo1 = new CultureInfo("en-US", false).TextInfo;
                        wftype = textInfo.ToLower(wftype);
                        if (wftype == "wfregion")
                        {
                            Console.WriteLine("Select by code, date, both, or none?");
                            choice = Console.ReadLine();
                            switch (choice)
                            {
                                case "code":
                                    Console.WriteLine("Select by code1 or code2?");
                                    string codeCol = Console.ReadLine();
                                    Console.WriteLine("Enter desired code");
                                    string code = Console.ReadLine();
                                    cmd = new SqlCommand("SELECT * FROM Staff WHERE (Staff." + @codeCol + "='" + @code + "') AND staffType ='WFRegion' FOR XML PATH('wrStaff'), ROOT('kcpsWorkforceStaff')", conn);
                                    quit = true;
                                    break;

                                case "date":
                                    Console.WriteLine("Enter desired date using the format MM/DD/YYYY.");
                                    string date = Console.ReadLine();
                                    cmd = new SqlCommand("SELECT * FROM Staff WHERE (Staff.date_created >= '" + @date + "' OR Staff.date_updated >= '" + @date + "') AND staffType ='WFRegion' FOR XML PATH('wrStaff'), ROOT('kcpsWorkforceStaff')", conn);
                                    quit = true;
                                    break;

                                case "both":
                                    cmd = new SqlCommand("SELECT * FROM Staff WHERE staffType ='WFRegion' FOR XML PATH('wrStaff'), ROOT('kcpsWorkforceStaff')", conn);
                                    quit = true;
                                    break;

                                case "none":
                                    cmd = new SqlCommand("SELECT * FROM Staff WHERE staffType ='WFRegion' FOR XML PATH('wrStaff'), ROOT('kcpsWorkforceStaff')", conn);
                                    quit = true;
                                    break;

                                default:
                                    Console.WriteLine("Unknown Command " + choice);
                                    continue;
                            }
                        }
                        else if (wftype == "wfcenter")
                        {
                            Console.WriteLine("Select by code, date, both, or none?");
                            choice = Console.ReadLine();
                            switch (choice)
                            {
                                case "code":
                                    Console.WriteLine("Select by code1 or code2?");
                                    string codeCol = Console.ReadLine();
                                    Console.WriteLine("Enter desired code");
                                    string code = Console.ReadLine();
                                    cmd = new SqlCommand("SELECT * FROM Staff WHERE (Staff." + @codeCol + "='" + @code + "') AND staffType ='WFCenter' FOR XML PATH('wcStaff'), ROOT('kcpsWorkforceStaff')", conn);
                                    quit = true;
                                    break;

                                case "date":
                                    Console.WriteLine("Enter desired date using the format MM/DD/YYYY.");
                                    string date = Console.ReadLine();
                                    cmd = new SqlCommand("SELECT * FROM Staff WHERE (Staff.date_created >= '" + @date + "' OR Staff.date_updated >= '" + @date + "') AND staffType ='WFCenter' FOR XML PATH('wcStaff'), ROOT('kcpsWorkforceStaff')", conn);
                                    quit = true;
                                    break;

                                case "both":
                                    cmd = new SqlCommand("SELECT * FROM Staff WHERE staffType ='WFCenter' FOR XML PATH('wcStaff'), ROOT('kcpsWorkforceStaff')", conn);
                                    quit = true;
                                    break;

                                case "none":
                                    cmd = new SqlCommand("SELECT * FROM Staff WHERE staffType ='WFCenter' FOR XML PATH('wcStaff'), ROOT('kcpsWorkforceStaff')", conn);
                                    quit = true;
                                    break;

                                default:
                                    Console.WriteLine("Unknown Command " + choice);
                                    continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Unknown Command " + wftype);
                            continue;
                        }
                    }
                    else if (staffType == "all")
                    {
                        Console.WriteLine("Select by code, date, both, or none?");
                        choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "code":
                                Console.WriteLine("Select by code1 or code2?");
                                string codeCol = Console.ReadLine();
                                Console.WriteLine("Enter desired code");
                                string code = Console.ReadLine();
                                cmd = new SqlCommand("SELECT * FROM Staff  WHERE (Staff." + @codeCol + "='" + @code + "') FOR XML PATH('staff'), ROOT('k12Staff')", conn);
                                quit = true;
                                break;

                            case "date":
                                Console.WriteLine("Enter desired date using the format MM/DD/YYYY.");
                                string date = Console.ReadLine();
                                cmd = new SqlCommand("SELECT * FROM Staff WHERE (Staff.date_created >= '" + @date + "' OR Staff.date_updated >= '" + @date + "') FOR XML PATH('staff'), ROOT('k12Staff')", conn);
                                quit = true;
                                break;

                            case "both":
                                cmd = new SqlCommand("SELECT * FROM Staff FOR XML PATH('staff'), ROOT('k12Staff')", conn);
                                quit = true;
                                break;

                            case "none":
                                cmd = new SqlCommand("SELECT * FROM Staff FOR XML PATH('staff'), ROOT('k12Staff')", conn);
                                quit = true;
                                break;

                            default:
                                Console.WriteLine("Unknown Command " + choice);
                                continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unknown Command " + staffType);
                        continue;
                    }
                }
                using (cmd)
                {
                    using (var reader = cmd.ExecuteXmlReader())
                    {
                        var doc = new XDocument();
                        try
                        {
                            doc = XDocument.Load(reader);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("There are no entries that match the given parameters.");
                        }

                        switch(staff)
                        {
                            case "k12staff":
                                path = @"K12Staff." + DateTime.Now.ToString("yyyyMMdd") + ".xml";
                                break;
                            case "psstaff":
                                path = @"psStaff." + DateTime.Now.ToString("yyyyMMdd") + ".xml";
                                break;
                            case "wfstaff":
                                if (wftype == "wfregion")
                                {
                                    path = @"WFRegionStaff." + DateTime.Now.ToString("yyyyMMdd") + ".xml";
                                    wfid = "WFREGID";
                                }
                                else if(wftype == "wfcenter")
                                {
                                    path = @"WFCenterStaff." + DateTime.Now.ToString("yyyyMMdd") + ".xml";
                                    wfid = "WFID";
                                }
                                break;
                            default:
                                path = @"Staff." + DateTime.Now.ToString("yyyyMMdd") + ".xml";
                                break;
                        }
                        using (var writer = new StreamWriter(path))
                        {
                            if (staff == "k12staff" || staff == "all")
                            {
                                XNamespace ns = "http://specification.sifassociation.org/Implementation/na/3.2/html/CEDS/K12/K12_k12Staff.html";
                                var root = new XElement(ns + "k12Staff");
                                int count = 0;
                                foreach (var d in doc.Descendants("staff"))
                                {
                                    //SimplerAES item = new SimplerAES();
                                    //string dhash = item.Decrypt((string)d.Element("password"));
                                    string indicator;
                                    string ncesid;
                                    string delete;
                                    string first;
                                    string last;
                                    string position;
                                    string leaID;
                                    string schoolID;
                                    string phone;
                                    string email;

                                    /*Phone number indication*/
                                    if ((string)d.Element("phoneNumber") != null)
                                    {
                                        indicator = "Yes";
                                    }
                                    else
                                    {
                                        indicator = "No";
                                    }

                                    /*NCESID modification*/
                                    if ((string)d.Element("leaID") == "")
                                    {
                                        ncesid = (string)d.Element("NCESID_district") + (string)d.Element("NCESID_school");
                                    }
                                    else
                                    {
                                        ncesid = ncesid = (string)d.Element("NCESID_district");
                                    }

                                    /*Delete flag modification*/
                                    if ((string)d.Element("delete_flag") == "Y")
                                    {
                                        delete = "1";
                                    }
                                    else
                                    {
                                        delete = "0";
                                    }

                                    /*first name check for null*/
                                    if ((string)d.Element("firstName") == null)
                                    {
                                        first = string.Empty;
                                    }
                                    else
                                    {
                                        first = (string)d.Element("firstName");
                                    }

                                    /*last name check for null*/
                                    if ((string)d.Element("lastName") == null)
                                    {
                                        last = string.Empty;
                                    }
                                    else
                                    {
                                        last = (string)d.Element("lastName");
                                    }

                                    /*position check for null*/
                                    if ((string)d.Element("positionTitle") == null)
                                    {
                                        position = string.Empty;
                                    }
                                    else
                                    {
                                        position = (string)d.Element("positionTitle");
                                    }

                                    /*leaID check for null*/
                                    if ((string)d.Element("leaID") == null)
                                    {
                                        leaID = string.Empty;
                                    }
                                    else
                                    {
                                        leaID = (string)d.Element("leaID");
                                    }

                                    /*schoolID check for null*/
                                    if ((string)d.Element("schoolID") == null)
                                    {
                                        schoolID = string.Empty;
                                    }
                                    else
                                    {
                                        schoolID = (string)d.Element("schoolID");
                                    }

                                    /*phone check for null*/
                                    if ((string)d.Element("phoneNumber") == null)
                                    {
                                        phone = string.Empty;
                                    }
                                    else
                                    {
                                        phone = (string)d.Element("phoneNumber");
                                    }

                                    /*email check for null*/
                                    if ((string)d.Element("email") == null)
                                    {
                                        email = string.Empty;
                                    }
                                    else
                                    {
                                        email = (string)d.Element("email");
                                    }
                                    count++;
                                    root.Add(new XElement(ns + "staff",
                                                new XElement(ns + "identity",
                                                    new XElement(ns + "name",
                                                        new XElement(ns + "firstName", first),
                                                        new XElement(ns + "lastName", last)
                                                        )
                                                    ),
                                                new XElement(ns + "employment",
                                                    new XElement(ns + "positionTitle", position)
                                                        ),
                                                new XElement(ns + "assignment",
                                                    new XElement(ns + "leaID", leaID),
                                                    new XElement(ns + "schoolID", schoolID)
                                                            ),
                                                new XElement(ns + "contact",
                                                    new XElement(ns + "phoneNumberList",
                                                        new XElement(ns + "phoneNumber", phone),
                                                        new XElement(ns + "phoneNumberIndicator", indicator)
                                                    ),
                                                    new XElement(ns + "emailList",
                                                        new XElement(ns + "email", email)
                                                        )
                                                        ),
                                                new XElement(ns + "delete", delete)
                                                //new XElement(ns + "access",
                                                //new XElement(ns + "username", (string)d.Element("username")),
                                                //new XElement(ns + "password", dhash),
                                                //new XElement(ns + "NCESID", ncesid)
                                                //)


                                                )
                                                );
                                }
                                root.Save(writer);
                                Console.WriteLine("" + count + " staff records written");
                                Console.ReadLine();
                            }
                            else if(staff == "psstaff")
                            {
                                XNamespace ns = "http://specification.sifassociation.org/Implementation/na/3.2/html/CEDS/PostSecondary/PostSecondary_psStaffType.html";
                                var root = new XElement(ns + "psStaffType");
                                int count = 0;
                                foreach (var d in doc.Descendants("psStaff"))
                                {
                                    //SimplerAES item = new SimplerAES();
                                    //string dhash = item.Decrypt((string)d.Element("password"));
                                    string indicator;
                                    string ipedsid;
                                    string delete;
                                    string first;
                                    string last;
                                    string position;
                                    string phone;
                                    string email;

                                    /*Phone number indication*/
                                    if ((string)d.Element("phoneNumber") != null)
                                    {
                                        indicator = "Yes";
                                    }
                                    else
                                    {
                                        indicator = "No";
                                    }

                                    /*Delete flag modification*/
                                    if ((string)d.Element("delete_flag") == "Y")
                                    {
                                        delete = "1";
                                    }
                                    else
                                    {
                                        delete = "0";
                                    }

                                    /*first name check for null*/
                                    if ((string)d.Element("firstName") == null)
                                    {
                                        first = string.Empty;
                                    }
                                    else
                                    {
                                        first = (string)d.Element("firstName");
                                    }

                                    /*last name check for null*/
                                    if ((string)d.Element("lastName") == null)
                                    {
                                        last = string.Empty;
                                    }
                                    else
                                    {
                                        last = (string)d.Element("lastName");
                                    }

                                    /*position check for null*/
                                    if ((string)d.Element("positionTitle") == null)
                                    {
                                        position = string.Empty;
                                    }
                                    else
                                    {
                                        position = (string)d.Element("positionTitle");
                                    }

                                    /*ipedsid check for null*/
                                    if ((string)d.Element("psi_campus") == null)
                                    {
                                        ipedsid = (string)d.Element("psi_ipedsID");
                                    }
                                    else
                                    {
                                        ipedsid = ((string)d.Element("psi_ipedsID") + "-" + (string)d.Element("psi_campus"));
                                    }

                                    /*phone check for null*/
                                    if ((string)d.Element("phoneNumber") == null)
                                    {
                                        phone = string.Empty;
                                    }
                                    else
                                    {
                                        phone = (string)d.Element("phoneNumber");
                                    }

                                    /*email check for null*/
                                    if ((string)d.Element("email") == null)
                                    {
                                        email = string.Empty;
                                    }
                                    else
                                    {
                                        email = (string)d.Element("email");
                                    }
                                    count++;
                                    root.Add(new XElement(ns + "psStaff",
                                                new XElement(ns + "identity",
                                                    new XElement(ns + "name",
                                                        new XElement(ns + "firstName", first),
                                                        new XElement(ns + "lastName", last)
                                                        )
                                                    ),
                                                new XElement(ns + "access",
                                                    new XElement(ns + "positionTitle", position),
                                                    new XElement(ns + "phoneNumber", phone),
                                                    new XElement(ns + "phoneNumberIndicator", indicator),
                                                    new XElement(ns + "email", email),
                                                    new XElement(ns + "IPEDSID", ipedsid)
                                                    ),
                                                new XElement(ns + "delete", delete)
                                                )
                                                );
                                }
                                root.Save(writer);
                                Console.WriteLine("" + count + " staff records written");
                                Console.ReadLine();
                            }
                            else if (staff == "wfstaff")
                            {
                                string wft;
                                if(wftype == "wfregion")
                                {
                                    wft = "wrStaff";
                                }
                                else
                                {
                                    wft = "wcStaff";
                                }
                                XNamespace ns = "link placeholder";
                                var root = new XElement(ns + "kcpsWorkforceStaff");
                                int count = 0;
                                foreach (var d in doc.Descendants(wft))
                                {
                                    //SimplerAES item = new SimplerAES();
                                    //string dhash = item.Decrypt((string)d.Element("password"));
                                    string indicator;
                                    string id;
                                    string delete;
                                    string first;
                                    string last;
                                    string position;
                                    string phone;
                                    string email;

                                    /*Phone number indication*/
                                    if ((string)d.Element("phoneNumber") != null)
                                    {
                                        indicator = "Yes";
                                    }
                                    else
                                    {
                                        indicator = "No";
                                    }

                                    /*Delete flag modification*/
                                    if ((string)d.Element("delete_flag") == "Y")
                                    {
                                        delete = "1";
                                    }
                                    else
                                    {
                                        delete = "0";
                                    }

                                    /*first name check for null*/
                                    if ((string)d.Element("firstName") == null)
                                    {
                                        first = string.Empty;
                                    }
                                    else
                                    {
                                        first = (string)d.Element("firstName");
                                    }

                                    /*last name check for null*/
                                    if ((string)d.Element("lastName") == null)
                                    {
                                        last = string.Empty;
                                    }
                                    else
                                    {
                                        last = (string)d.Element("lastName");
                                    }

                                    /*position check for null*/
                                    if ((string)d.Element("positionTitle") == null)
                                    {
                                        position = string.Empty;
                                    }
                                    else
                                    {
                                        position = (string)d.Element("positionTitle");
                                    }

                                    /*id check for null*/
                                    if ((string)d.Element("WFCID") == null)
                                    {
                                        id = (string)d.Element("WFRID");
                                    }
                                    else if((string)d.Element("WFRID") == null)
                                    {
                                        id = (string)d.Element("WFCID");
                                    }
                                    else
                                    {
                                        id = string.Empty;
                                    }

                                    /*phone check for null*/
                                    if ((string)d.Element("phoneNumber") == null)
                                    {
                                        phone = string.Empty;
                                    }
                                    else
                                    {
                                        phone = (string)d.Element("phoneNumber");
                                    }

                                    /*email check for null*/
                                    if ((string)d.Element("email") == null)
                                    {
                                        email = string.Empty;
                                    }
                                    else
                                    {
                                        email = (string)d.Element("email");
                                    }
                                    count++;
                                    root.Add(new XElement(ns + wft,
                                                new XElement(ns + "identity",
                                                    new XElement(ns + "name",
                                                        new XElement(ns + "firstName", first),
                                                        new XElement(ns + "lastName", last)
                                                        )
                                                    ),
                                                new XElement(ns + "access",
                                                    new XElement(ns + "positionTitle", position),
                                                    new XElement(ns + "phoneNumber", phone),
                                                    new XElement(ns + "phoneNumberIndicator", indicator),
                                                    new XElement(ns + "email", email),
                                                    new XElement(ns + wfid, id)
                                                    ),
                                                new XElement(ns + "delete", delete)
                                                )
                                                );
                                }
                                root.Save(writer);
                                Console.WriteLine("" + count + " staff records written");
                                Console.ReadLine();
                            }

                        }


                    }
                }
            }

        }
    }
}
