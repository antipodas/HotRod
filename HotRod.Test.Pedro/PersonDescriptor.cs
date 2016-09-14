using System;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace HotRod.Test.Pedro
{
    public static class PersonDescriptor
    {
        private static FileDescriptor descriptor;
        static PersonDescriptor()
        {
            GeneratedCodeInfo[] infoArray1 = new GeneratedCodeInfo[2];
            string[] textArray1 = new string[] { "Name", "Id", "Surname" };
            infoArray1[0] = new GeneratedCodeInfo(typeof(Person), Person.Parser, textArray1, null, null, null);
            string[] textArray2 = new string[] { "Person" };
            infoArray1[1] = new GeneratedCodeInfo(typeof(Person), Person.Parser, textArray2, null, null, null);
            descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChFhZGRyZXNzYm9vay5wcm90bxIIdHV0b3JpYWwiMQoGUGVyc29uEgwKBG5h" + "bWUYASABKAkSCgoCaWQYAiABKAUSDQoFZW1haWwYAyABKAkiLwoLQWRkcmVz" + "c0Jvb2sSIAoGcGVyc29uGAEgAygLMhAudHV0b3JpYWwuUGVyc29uQikKFGNv" + "bS5leGFtcGxlLnR1dG9yaWFsQhFBZGRyZXNzQm9va1Byb3Rvc2IGcHJvdG8z"), new FileDescriptor[0], new GeneratedCodeInfo(null, infoArray1));
        }

        // Properties
        public static FileDescriptor Descriptor =>
            descriptor;


    }
}