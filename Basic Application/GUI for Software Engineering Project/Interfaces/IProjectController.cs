﻿using GUI_for_Software_Engineering_Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_for_Software_Engineering_Project
{
    interface IProjectController
    {

        IProject_Window view { get; }

        List<IAssetData> AssetSource { get; }

<<<<<<< HEAD
        void UploadFile();

        void DownloadFile(IAssetData data);

=======
>>>>>>> EdsBranch
        void PreviewAsset(IAssetData data);

    }
}
