using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex;
using Cognex.VisionPro;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.Caliper;
using Cognex.VisionPro.CNLSearch;
using Cognex.VisionPro.ColorMatch;
using Cognex.VisionPro.Dimensioning;
using Cognex.VisionPro.ID;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.OC;
using Cognex.VisionPro.OCRMax;
using Cognex.VisionPro.OCVMax;
using Cognex.VisionPro.PatInspect;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.PMRedLine;
using Cognex.VisionPro.SearchMax;
using Cognex.VisionPro.ToolBlock;

namespace Hong_Solution
{
    public class VisionProClass
    {
        public CogToolBlock tmpToolBlock = null;
        public CogToolBlock tmpToolBlock2 = null;
        public void LoadToolblock(object obj)
        {
            Array Obj = (Array)obj;
            string path = (string)Obj.GetValue(0);
            int index = (int)Obj.GetValue(1);
            using (FileStream fsSource = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                switch (index)
                {
                    case 0:
                        if (tmpToolBlock == null)
                        {
                            tmpToolBlock = (CogToolBlock)CogSerializer.LoadObjectFromStream(fsSource);
                        }
                        else
                        {
                            tmpToolBlock.Dispose();
                            tmpToolBlock = null;
                            tmpToolBlock = (CogToolBlock)CogSerializer.LoadObjectFromStream(fsSource);
                        }
                        break;
                    case 1:

                        if (tmpToolBlock2 == null)
                        {
                            tmpToolBlock2 = (CogToolBlock)CogSerializer.LoadObjectFromStream(fsSource);
                        }
                        else
                        {
                            tmpToolBlock2.Dispose();
                            tmpToolBlock2 = null;
                            tmpToolBlock2 = (CogToolBlock)CogSerializer.LoadObjectFromStream(fsSource);
                        }
                        break;
                    default:

                        break;
                }

            }
        }
        public void AddToolControl(Panel panel)
        {
            List<CogToolEditControlBase> vprocontol1 = new List<CogToolEditControlBase>();

            List<CogToolEditControlBaseV2> vprocontol = new List<CogToolEditControlBaseV2>();
            CogFindLineEditV2 cogFindLineEdit1 = new CogFindLineEditV2();
            CogIPOneImageEditV2 cogIPOneImageEdit = new CogIPOneImageEditV2();
            CogFixtureEditV2 cogFiextureEdit = new CogFixtureEditV2();
            CogHistogramEditV2 cogHistogramEdit = new CogHistogramEditV2();
            CogBlobEditV2 cogBlobEdit = new CogBlobEditV2();
            CogCNLSearchEditV2 cogCNLEdit = new CogCNLSearchEditV2();
            CogIntersectSegmentSegmentEditV2 CogIntersectSegmentSegment = new CogIntersectSegmentSegmentEditV2();
            CogPMAlignEditV2 cogPMAlign = new CogPMAlignEditV2();
            CogPMAlignMultiEditV2 CogPMAlignMulti = new CogPMAlignMultiEditV2();
            CogImageFileEditV2 CogImageFile = new CogImageFileEditV2();
            CogCreateGraphicLabelEditV2 CogCreateGraphicLabel= new CogCreateGraphicLabelEditV2();
            CogFindCircleEditV2 CogFindCircle= new CogFindCircleEditV2();
            CogFindCornerEditV2 CogFindCorner= new CogFindCornerEditV2();
            CogFindEllipseEditV2 CogFindEllipse= new CogFindEllipseEditV2();
            CogFitCircleEditV2 CogFitCircle= new CogFitCircleEditV2();
            CogIDEditV2 CogID= new CogIDEditV2();
            CogOCRMaxEditV2 CogOCRMaxEdit= new CogOCRMaxEditV2();
            CogOCVMaxEdit CogOCVMaxEdit = new CogOCVMaxEdit();
            CogAffineTransformEditV2 CogAffineTransformEdit = new CogAffineTransformEditV2();
            CogCopyRegionEditV2 CogCopyRegionEdit = new CogCopyRegionEditV2();
            CogImageConvertEditV2 CogImageConvertEdit = new CogImageConvertEditV2();
            CogToolBlockEditV2 CogToolBlockEdit = new CogToolBlockEditV2();
            CogColorMatchEditV2 CogColorMatchEdit = new CogColorMatchEditV2();
            CogCaliperEditV2 CogCaliperEdit = new CogCaliperEditV2();
            CogDataAnalysisEditV2 CogDataAnalysisEdit = new CogDataAnalysisEditV2();
            CogPatInspectEditV2 CogPatInspectEdit = new CogPatInspectEditV2();
            CogPMRedLineEditV2 CogPMRedLineEdit = new CogPMRedLineEditV2();
            CogSearchMaxEditV2 CogSearchMaxEdit = new CogSearchMaxEditV2();
            CogAnglePointPointEditV2 CogAnglePointPointEdit = new CogAnglePointPointEditV2();
            vprocontol.Add(cogFindLineEdit1);
            vprocontol.Add(cogIPOneImageEdit);
            vprocontol.Add(cogFiextureEdit);
            vprocontol.Add(cogHistogramEdit);
            vprocontol.Add(cogBlobEdit);
            vprocontol.Add(cogCNLEdit);
            vprocontol.Add(CogIntersectSegmentSegment);
            vprocontol.Add(cogPMAlign);
            vprocontol.Add(CogPMAlignMulti);
            vprocontol.Add(CogImageFile);
            vprocontol.Add(CogCreateGraphicLabel);
            vprocontol.Add(CogFindCircle);
            vprocontol.Add(CogFindCorner);
            vprocontol.Add(CogFindEllipse);
            vprocontol.Add(CogFitCircle);
            vprocontol.Add(CogID);
            vprocontol.Add(CogOCRMaxEdit);
            vprocontol.Add(CogAffineTransformEdit);
            vprocontol.Add(CogCopyRegionEdit);
            vprocontol.Add(CogImageConvertEdit);
            vprocontol.Add(CogToolBlockEdit);
            vprocontol.Add(CogColorMatchEdit); 
            vprocontol.Add(CogCaliperEdit);
            vprocontol.Add(CogDataAnalysisEdit);
            vprocontol.Add(CogPatInspectEdit);
            vprocontol.Add(CogPMRedLineEdit);
            vprocontol.Add(CogSearchMaxEdit);
            vprocontol.Add(CogAnglePointPointEdit);

            vprocontol1.Add(CogOCVMaxEdit);
            foreach (var control in vprocontol)
            {
                SetControl(control, panel);
                panel.Controls.Add(control);
            }
            foreach (var control in vprocontol1)
            {
                SetControl(control, panel);
                panel.Controls.Add(control);
            }
        }
        public void SetControl(CogToolEditControlBaseV2 control,Panel panel)
        {
            control.Location= new Point(0, 0);
            control.Size=panel.Size;
            control.Visible=false;
        }
        public void SetControl(CogToolEditControlBase control, Panel panel)
        {
            control.Location = new Point(0, 0);
            control.Size = panel.Size;
            control.Visible = false;
        }
        public List<string> LoadToolList()
        {
            List<string> toolList = new List<string>();
           
            try
            {
                foreach (ICogTool ToolName in tmpToolBlock.Tools)
                {
                    toolList.Add(ToolName.Name);
                }
                return toolList;
            }
            catch (NullReferenceException nx)
            {
                MessageBox.Show("Job파일을 열어주세요", "Job파일을 찾을 수 없습니다.");
                return toolList;
            }
        }

        public void OpenToolScript(CogToolBlock Toolblock)
        {
            if (Toolblock.Script != null)
            {   
                Toolblock.EditExistingScript();
            }   
            else
            {   
                Toolblock.CreateNewScript(Cognex.VisionPro.Implementation.Internal.CogScriptLanguageConstants.ScriptCSharp,CogToolBlockScriptTypeConstants.Simple);
                Toolblock.EditExistingScript();
            }
        }
        private void AddToolToBlock(CogToolBlock ToolBlock, ICogTool cogTool)
        {
            ToolBlock.Tools.Add(cogTool);
            
        }
    }
}
