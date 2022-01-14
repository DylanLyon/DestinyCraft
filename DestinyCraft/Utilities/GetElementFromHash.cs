namespace DestinyCraft.Utilities
{
    public class GetElementFromHash
    {
        public string GetElementFromString(string hash)
        {
            switch(hash) {
                case "1198124803": // Any
                    return "universal";
                case "728351493": // Arc
                    return "arc";
                case "591714140": // Solar
                    return "solar";
                case "4069572561": // Void
                    return "void";
                case "1819698290": // Stasis
                    return "stasis";
                default:
                    return "universal";
            }
        }
        
        // public string GetElementFromInt(int hash)
        // {
        //     switch(hash) {
        //         case 1198124803: // Any
        //             return "universal";
        //         case 728351493: // Arc
        //             return "arc";
        //         case 591714140: // Solar
        //             return "solar";
        //         case 4069572561: // Void
        //             return "void";
        //         case 1819698290: // Stasis
        //             return "stasis";
        //         default:
        //             return "universal";
        //     }
        // }
    }
}