using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using System.IO;
using System.Media;

using Sigbit.Lib.Translator.Microsoft;

public partial class TranslatorSample : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        ddlbFromLanguage.Items.Clear();
        ddlbFromLanguage.Items.Add(new ListItem(TranslateLanguage.English));
        ddlbFromLanguage.Items.Add(new ListItem(TranslateLanguage.SimplifiedChinese));
        ddlbFromLanguage.Items.Add(new ListItem(TranslateLanguage.TraditionalChinese));
        ddlbFromLanguage.Items.Add(new ListItem(TranslateLanguage.Japanese));
        ddlbFromLanguage.Items.Add(new ListItem(TranslateLanguage.Korean));
        ddlbFromLanguage.Items.Add(new ListItem(TranslateLanguage.Deutsch));
        ddlbFromLanguage.Items.Add(new ListItem(TranslateLanguage.French));
        ddlbFromLanguage.Items.Add(new ListItem(TranslateLanguage.Spanish));
        ddlbFromLanguage.SelectedIndex = 1;

        ddlbToLanguage.Items.Clear();
        ddlbToLanguage.Items.Add(new ListItem(TranslateLanguage.English));
        ddlbToLanguage.Items.Add(new ListItem(TranslateLanguage.SimplifiedChinese));
        ddlbToLanguage.Items.Add(new ListItem(TranslateLanguage.TraditionalChinese));
        ddlbToLanguage.Items.Add(new ListItem(TranslateLanguage.Japanese));
        ddlbToLanguage.Items.Add(new ListItem(TranslateLanguage.Korean));
        ddlbToLanguage.Items.Add(new ListItem(TranslateLanguage.Deutsch));
        ddlbToLanguage.Items.Add(new ListItem(TranslateLanguage.French));
        ddlbToLanguage.Items.Add(new ListItem(TranslateLanguage.Spanish));
        ddlbToLanguage.SelectedIndex = 0;

        ddlbSpeakLanguage.Items.Clear();
        ddlbSpeakLanguage.Items.Add(new ListItem(TranslateLanguage.English));
        ddlbSpeakLanguage.Items.Add(new ListItem(TranslateLanguage.SimplifiedChinese));
        ddlbSpeakLanguage.Items.Add(new ListItem(TranslateLanguage.TraditionalChinese));
        ddlbSpeakLanguage.Items.Add(new ListItem(TranslateLanguage.Japanese));
        ddlbSpeakLanguage.Items.Add(new ListItem(TranslateLanguage.Korean));
        ddlbSpeakLanguage.Items.Add(new ListItem(TranslateLanguage.Deutsch));
        ddlbSpeakLanguage.Items.Add(new ListItem(TranslateLanguage.French));
        ddlbSpeakLanguage.Items.Add(new ListItem(TranslateLanguage.Spanish));
        ddlbSpeakLanguage.SelectedIndex = 1;

        ddlbQuality.Items.Clear();
        ddlbQuality.Items.Add(new ListItem("Max Size", "max"));
        ddlbQuality.Items.Add(new ListItem("Min Size", "min"));

        ddlbFormat.Items.Clear();
        ddlbFormat.Items.Add(new ListItem("WAV", "wav"));
        ddlbFormat.Items.Add(new ListItem("MP3", "mp3"));

    }

    protected void btnDoTranslate_Click(object sender, EventArgs e1)
    {
        TranslateService trans = new TranslateService("sigbit_siri", "4Gbkqbsa2fPk9eWUn+2WLhkwnoEGxFmiD62igFD0fpE=");
        edtResult.Text = trans.Translate(edtQueryText.Text, ddlbFromLanguage.SelectedItem.Value, ddlbToLanguage.SelectedItem.Value);
    }



    protected void btnDetectLanguage_Click(object sender, EventArgs e)
    {
        TranslateService trans = new TranslateService("sigbit_siri", "4Gbkqbsa2fPk9eWUn+2WLhkwnoEGxFmiD62igFD0fpE=");
        edtResult.Text = trans.DetectLanguage(edtQueryText.Text);
    }

    protected void btnSpeak_Click(object sender, EventArgs e)
    {
        TranslateService trans = new TranslateService("sigbit_siri", "4Gbkqbsa2fPk9eWUn+2WLhkwnoEGxFmiD62igFD0fpE=");

        SpeakResultFormat format = SpeakResultFormat.WAV;
        if (ddlbFormat.SelectedValue == "mp3")
            format = SpeakResultFormat.MP3;

        SpeakResultQuality quality = SpeakResultQuality.MaxQuality;
        if (ddlbQuality.SelectedValue == "min")
            quality = SpeakResultQuality.MinSize;

        string sSaveFileName = "d:\\test." + ddlbFormat.SelectedValue.ToString();

        if (trans.Speak(edtQueryText.Text, ddlbSpeakLanguage.SelectedValue, sSaveFileName, format, quality))
            edtResult.Text = "voice file save to " + sSaveFileName;
        else
            edtResult.Text = "speak text fail";
    }
}
