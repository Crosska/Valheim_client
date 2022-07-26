using BytesRoad.Net.Ftp;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace Client
{
    public partial class MainForm : Form
    {

        private string recentGamePath = "";
        private int TimeoutFTP = 1000000; // Таймаут FTP подключения
        private string FTP_SERVER = "176.57.173.245"; // IP адрес FTP сервера
        private int FTP_PORT = 28231; // Порт FTP сервера
        private string FTP_USER = "gpftp2440045883081933"; // Имя пользователя FTP сервера
        private string FTP_PASSWORD = "QTEeWSXi"; // Пароль пользователя FTP сервера
        private List<String> filesInDir = new List<String>(); // Список главных путей с названиями файлов
        private List<String> directoriesInDir = new List<String>(); // Список путей с названиями папками
        private List<String> mainDirectoriesInDir = new List<String>(); // Список путей с названиями папками
        private FtpClient client = new FtpClient();

        public MainForm()
        {
            InitializeComponent();

            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += ProgressChanged;
            backgroundWorker.DoWork += DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (StreamReader sr = File.OpenText(Path.GetTempPath() + "ValheimGamePath.txt"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        recentGamePath = s;
                    }
                }
                //MessageBox.Show(recentGamePath);
                if (!recentGamePath.Equals(""))
                {
                    directoryPathTextBox.Text = recentGamePath;
                }
            }
            catch (Exception)
            {

            }

        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            if (directoryPathTextBox.Text == "") // Если путь к папке с игрой пустой
            {
                MessageBox.Show("Вы не указали папку с игрой!");
            }
            else if (!Directory.Exists(directoryPathTextBox.Text)) // Если указанного пути не существует
            {
                MessageBox.Show("Указанной папки не существует!");
            }
            else if (!File.Exists(directoryPathTextBox.Text + "\\valheim.exe"))
            {
                MessageBox.Show("Вы указали неправильную папку!");
            }
            else
            {

                //Задаём параметры клиента.
                client.PassiveMode = true; //Включаем пассивный режим.

                //Подключаемся к FTP серверу.

                client.Connect(TimeoutFTP * 10, FTP_SERVER, FTP_PORT);
                client.Login(TimeoutFTP * 10, FTP_USER, FTP_PASSWORD);

                try
                {
                    indexingAllFiles(client); // Проход по FTP серверу с записью всех папок и файлов



                    if (checkExistedFiles(client))
                    {
                        Process.Start(directoryPathTextBox.Text + "\\valheim.exe");
                    }
                    else
                    {

                        backgroundWorker.RunWorkerAsync();

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Возникла ошибка при скачивании файлов. Проверьте подключение к интернету.");
                    MessageBox.Show(ex.ToString(), "Ошибка");
                    loadingUpdateProgressBar.Value = 0;
                    client.Disconnect(TimeoutFTP);
                }

            }

        }

        private BackgroundWorker backgroundWorker = new BackgroundWorker();

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            manualDetectGamePathRadioButton.Enabled = false;
            autoDetectGamePathRadioButton.Enabled = false;
            startGameButton.Enabled = false;
            try
            {
                DirectoryInfo dirToDelete = new DirectoryInfo(directoryPathTextBox.Text + "\\BepInEx");
                dirToDelete.Delete();
            }
            catch (Exception)
            {
            }
            try
            {
                DirectoryInfo dirToDelete = new DirectoryInfo(directoryPathTextBox.Text + "\\unstripped_corlib");
                dirToDelete.Delete();
            }
            catch (Exception)
            {
            }
            try
            {
                DirectoryInfo dirToDelete = new DirectoryInfo(directoryPathTextBox.Text + "\\doorstop_libs");
                dirToDelete.Delete();
            }
            catch (Exception)
            {
            }
            loadingUpdateProgressBar.Value = 0;
            loadingUpdateProgressBar.Maximum = mainDirectoriesInDir.Count + directoriesInDir.Count + filesInDir.Count;

            foreach (string folderPath in mainDirectoriesInDir) // Создание основных директорий
            {

                string cutedPath = folderPath.Substring(15);
                string replacedCutedPath = cutedPath.Replace("/", "\\");
                currentProcessibleFile.Text = replacedCutedPath;
                //MessageBox.Show(directoryPathTextBox.Text + "\\" + replacedCutedPath);
                DirectoryInfo drInfo = new DirectoryInfo(directoryPathTextBox.Text + "\\" + replacedCutedPath);
                drInfo.Create();
                Thread.Sleep(100);
                loadingUpdateProgressBar.Value++;
            }

            Thread.Sleep(100);

            foreach (string folderPath in directoriesInDir) // Создание всех вторичных директорий
            {
                string cutedPath = folderPath.Substring(15);
                string replacedCutedPath = cutedPath.Replace("/", "\\");
                currentProcessibleFile.Text = replacedCutedPath;
                //MessageBox.Show(directoryPathTextBox.Text + "\\" + replacedCutedPath);
                DirectoryInfo drInfo = new DirectoryInfo(directoryPathTextBox.Text + "\\" + replacedCutedPath);
                drInfo.Create();
                Thread.Sleep(100);
                loadingUpdateProgressBar.Value++;
            }

            Thread.Sleep(100);

            foreach (string filePath in filesInDir) // Скачивание всех файлов с FTP сервера
            {
                string cutedPath = filePath.Substring(15);
                string replacedCutedPath = cutedPath.Replace("/", "\\");
                currentProcessibleFile.Text = replacedCutedPath;
                //MessageBox.Show(directoryPathTextBox.Text + "\\" + replacedCutedPath);
                client.GetFile(TimeoutFTP, directoryPathTextBox.Text + "\\" + replacedCutedPath, filePath);
                Thread.Sleep(100);
                loadingUpdateProgressBar.Value++;
            }

            Thread.Sleep(100);
            loadingUpdateProgressBar.Value = loadingUpdateProgressBar.Maximum;
            currentProcessibleFile.Text = "Готово";
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // This is called on the UI thread when ReportProgress method is called
            loadingUpdateProgressBar.Value = e.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            startGameButton.Enabled = true;
            manualDetectGamePathRadioButton.Enabled = true;
            autoDetectGamePathRadioButton.Enabled = true;

            client.Disconnect(TimeoutFTP);
            MessageBox.Show("Клиент успешно обновлен, далее будет запущена игра.", "Успех!");
            try
            {
                Process.Start(directoryPathTextBox.Text + "\\valheim.exe");
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка при запуске игры, запустите игру вручную.");
                }
            Process.GetCurrentProcess().Kill();
        }

        private bool checkExistedFiles(FtpClient client)
        {

            loadingUpdateProgressBar.Maximum = mainDirectoriesInDir.Count + directoriesInDir.Count + filesInDir.Count;

            foreach (string folder in mainDirectoriesInDir)
            {
                string cutedPath = folder.Substring(15);
                string replacedCutedPath = cutedPath.Replace("/", "\\");
                //MessageBox.Show(directoryPathTextBox.Text + "\\" + replacedCutedPath);

                if (!Directory.Exists(directoryPathTextBox.Text + "\\" + replacedCutedPath))
                {
                    MessageBox.Show("Обнаружены недостающие файлы, начинается перекачка клиента");
                    loadingUpdateProgressBar.Value = loadingUpdateProgressBar.Maximum;
                    return false;
                }
                loadingUpdateProgressBar.Value++;
            }
            foreach (string folder in directoriesInDir)
            {
                string cutedPath = folder.Substring(15);
                string replacedCutedPath = cutedPath.Replace("/", "\\");
                //MessageBox.Show(directoryPathTextBox.Text + "\\" + replacedCutedPath);
                if (!Directory.Exists(directoryPathTextBox.Text + "\\" + replacedCutedPath))
                {
                    MessageBox.Show("Обнаружены недостающие файлы, начинается перекачка клиента");
                    loadingUpdateProgressBar.Value = loadingUpdateProgressBar.Maximum;
                    return false;
                }
                loadingUpdateProgressBar.Value++;
            }
            foreach (string file in filesInDir)
            {
                string cutedPath = file.Substring(15);
                string replacedCutedPath = cutedPath.Replace("/", "\\");
                //MessageBox.Show(directoryPathTextBox.Text + "\\" + replacedCutedPath);
                if (!File.Exists(directoryPathTextBox.Text + "\\" + replacedCutedPath))
                {
                    MessageBox.Show("Обнаружены недостающие файлы, начинается перекачка клиента");
                    loadingUpdateProgressBar.Value = loadingUpdateProgressBar.Maximum;
                    return false;
                }
                loadingUpdateProgressBar.Value++;
            }

            return true;
        }

        private void indexingAllFiles(FtpClient client) // Метод скачивания всех файлов с директории LauncherFiles
        {
            string mainDirectory = "/LauncherFiles/"; // Начальная директория прохода
            try
            {
                client.ChangeDirectory(TimeoutFTP, mainDirectory); // Переход по указанной директории
                FtpItem[] listFiles = client.GetDirectoryList(TimeoutFTP); // Получение всех файлов по текущей директории
                foreach (FtpItem item in listFiles)
                {
                    if (item.ItemType == FtpItemType.Directory)
                    {
                        //MessageBox.Show("В список директорий добавлен: " + mainDirectory + item.Name.ToString());
                        mainDirectoriesInDir.Add(mainDirectory + item.Name.ToString());
                    }
                    else
                    {
                        filesInDir.Add(mainDirectory + item.Name.ToString());
                    }
                }
                foreach (string folder in mainDirectoriesInDir)
                {
                    //MessageBox.Show("Проверка директории: " + folder);
                    checkAnotherDir(folder, client);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка при скачивании файлов");
                client.Disconnect(TimeoutFTP);
            }
        }

        private void checkAnotherDir(string folder, FtpClient client)
        {
            List<String> tempDirList = new List<String>(); // Список путей с названиями папками
            try
            {
                client.ChangeDirectory(TimeoutFTP, folder); // Переход по указанной директории
                FtpItem[] listFiles = client.GetDirectoryList(TimeoutFTP); // Получение всех файлов по текущей директории
                foreach (FtpItem item in listFiles)
                {
                    if (item.ItemType == FtpItemType.Directory)
                    {
                        //MessageBox.Show("В список директорий добавлен (рекурсия): " + item.Name.ToString());
                        directoriesInDir.Add(folder + "/" + item.Name.ToString());
                        tempDirList.Add(folder + "/" + item.Name.ToString());
                    }
                    else
                    {
                        filesInDir.Add(folder + "/" + item.Name.ToString());
                    }
                }
                foreach (string folder_temp in tempDirList)
                {
                    //MessageBox.Show("Проверка директории (рекурсия): " + folder_temp);
                    checkAnotherDir(folder_temp, client);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка при скачивании файлов");
            }
        }

        private void chooseGameDirectoryButton_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog ofd = new FolderBrowserDialog();
            ofd.ShowDialog(); // открывает диалоговое окно для выбора приложения , пользователь выбирает , и нажимает "ок".
            string path = ofd.SelectedPath; // string path - это путь к приложению , которое выбрал пользователь
            if (!Directory.Exists(path)) // если этого приложению по указанному пути нет
            {
                //MessageBox.Show("Вы не выбрали папку."); // выполняется определенное действие
            }
            else // если есть
            {
                directoryPathTextBox.Text = path;
                try
                {
                    // Create the file, or overwrite if the file exists.
                    using (FileStream fs = File.Create(Path.GetTempPath() + "ValheimGamePath.txt"))
                    {
                        //MessageBox.Show(Path.GetTempPath() + "ValheimGamePath.txt");
                        byte[] info = new UTF8Encoding(true).GetBytes(path);
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void searchForGamePath()
        {
            string[] Drives = Environment.GetLogicalDrives();
            foreach (string driveLiteral in Drives)
            {
                if (Directory.Exists(driveLiteral + "Program Files (x86)\\Steam\\steamapps\\common\\Valheim"))
                {
                    directoryPathTextBox.Text = driveLiteral + "Program Files (x86)\\Steam\\steamapps\\common\\Valheim";
                    try
                    {
                        // Create the file, or overwrite if the file exists.
                        using (FileStream fs = File.Create(Path.GetTempPath() + "ValheimGamePath.txt"))
                        {
                            //MessageBox.Show(Path.GetTempPath() + "ValheimGamePath.txt");
                            byte[] info = new UTF8Encoding(true).GetBytes(driveLiteral + "Program Files (x86)\\Steam\\steamapps\\common\\Valheim");
                            // Add some information to the file.
                            fs.Write(info, 0, info.Length);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    return;
                }
                else if (Directory.Exists(driveLiteral + "SteamLibrary\\steamapps\\common\\Valheim"))
                {
                    directoryPathTextBox.Text = driveLiteral + "SteamLibrary\\steamapps\\common\\Valheim";
                    try
                    {
                        // Create the file, or overwrite if the file exists.
                        using (FileStream fs = File.Create(Path.GetTempPath() + "ValheimGamePath.txt"))
                        {
                            //MessageBox.Show(Path.GetTempPath() + "ValheimGamePath.txt");
                            byte[] info = new UTF8Encoding(true).GetBytes(driveLiteral + "SteamLibrary\\steamapps\\common\\Valheim");
                            // Add some information to the file.
                            fs.Write(info, 0, info.Length);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    return;
                }
            }
            if (directoryPathTextBox.Equals(""))
            {
                MessageBox.Show("Программа не обнаружила папку с игрой, укажите вручную.");
            }
        }

        private void autoDetectGamePathRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (autoDetectGamePathRadioButton.Checked)
            {
                searchForGamePath();
                panel1.Enabled = false;
            }
            else
            {
                panel1.Enabled = true;
            }
        }

        private void reloadDataButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Запущена полная перекачка файлов игры клиента");

            //Задаём параметры клиента.
            client.PassiveMode = true; //Включаем пассивный режим.

            //Подключаемся к FTP серверу.

            client.Connect(TimeoutFTP * 10, FTP_SERVER, FTP_PORT);
            client.Login(TimeoutFTP * 10, FTP_USER, FTP_PASSWORD);
            reload = true;
            backgroundWorker.RunWorkerAsync();
        }
    }

}