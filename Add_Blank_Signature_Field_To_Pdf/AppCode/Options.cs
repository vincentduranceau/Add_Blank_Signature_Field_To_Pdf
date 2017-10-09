using CommandLine;
using CommandLine.Text;

namespace Add_Blank_Signature_Field_To_Pdf.AppCode
{
    class Options
    {
        [Option('s', "source", Required = true,   HelpText = "Chemin du fichier à traiter. Obligatoire.")]
        public string srcFile { get; set; }

        [Option('d', "destination", Required = false,  HelpText = "Destination du fichier à traiter. Optionnel")]
        public string destFile { get; set; }

        [Option('x', "x", Required = true, HelpText = "Coordonnées x du cadre de signature. Obligatoire.")]
        public float x { get; set; }

        [Option('y', "y", Required = true, HelpText = "Coordonnées y du cadre de signature. Obligatoire.")]
        public float y { get; set; }

        [Option('w', "width", Required = true, HelpText = "Largeur du cadre de signature. Obligatoire.")]
        public float width { get; set; }

        [Option('h', "height", Required = true, HelpText = "Hauteur du cadre de signature. Obligatoire.")]
        public float height { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,(HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
