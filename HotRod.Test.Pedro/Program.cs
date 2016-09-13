using Google.Protobuf;
using Infinispan.HotRod;
using Infinispan.HotRod.Config;
using Infinispan.HotRod.Impl;
using Org.Infinispan.Protostream;
using Org.Infinispan.Query.Remote.Client;
using Pedro.Test.Hotrod;
using SampleBankAccount;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotRod.Test.Pedro
{
    class Program
    {
        static void Main(string[] args)
        {
            RemoteCacheManager remoteManager;
            const string ERRORS_KEY_SUFFIX = ".errors";
            const string PROTOBUF_METADATA_CACHE_NAME = "___protobuf_metadata";

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddServer()
                  .Host(args.Length > 1 ? args[0] : "127.0.0.1")
                  .Port(args.Length > 2 ? int.Parse(args[1]) : 11222);

            builder.Marshaller(new BasicTypesProtoStreamMarshaller());
            remoteManager = new RemoteCacheManager(builder.Build(), true);

            IRemoteCache<String, String> metadataCache = remoteManager.GetCache<String, String>(PROTOBUF_METADATA_CACHE_NAME);
            IRemoteCache<int, Person> testCache = remoteManager.GetCache<int, Person>("namedCache");

            // Installing the entities model into the Infinispan __protobuf_metadata cache 
            metadataCache.Put("sample_person/person.proto", File.ReadAllText("../../resources/proto2/person.proto"));
     
           // Console.WriteLine(File.ReadAllText("../../resources/proto2/person.proto"));
            if (metadataCache.ContainsKey(ERRORS_KEY_SUFFIX))
            {
                Console.WriteLine("fail: error in registering .proto model");
                Environment.Exit(-1);
            }

            testCache.Clear();
            // Fill the application cache
            Person person1 = new Person();
            person1.Id = 4;
            person1.Name = "Jerry";
            person1.Surname = "Mouse";
            testCache.Put(4, person1);



            // Run a query
            QueryRequest qr = new QueryRequest();
            qr.JpqlString = "from quickstart.Person";
            QueryResponse result = testCache.Query(qr);

            Console.WriteLine(result.NumResults);

            List<Person> listOfUsers = new List<Person>();
            if (unwrapResults(result, listOfUsers))
            {
                Console.WriteLine("It's not empty");
            }
            Console.WriteLine("There are " + listOfUsers.Count + " Users:");

            Console.WriteLine("did it finished?????");

            Thread.Sleep(2000);

        }

        // Convert Protobuf matter into C# objects
        private static bool unwrapResults<T>(QueryResponse resp, List<T> res) where T : IMessage<T>
        {
            if (resp.ProjectionSize > 0)
            {  // Query has select
                return false;
            }
            for (int i = 0; i < resp.NumResults; i++)
            {
                WrappedMessage wm = resp.Results.ElementAt(i);

                if (wm.WrappedBytes != null)
                {
                    WrappedMessage wmr = WrappedMessage.Parser.ParseFrom(wm.WrappedBytes);
                    if (wmr.WrappedMessageBytes != null)
                    {

                        System.Reflection.PropertyInfo pi = typeof(T).GetProperty("Parser");

                        MessageParser<T> p = (MessageParser<T>)pi.GetValue(null);
                        T u = p.ParseFrom(wmr.WrappedMessageBytes);
                        res.Add(u);
                    }
                }
            }
            return true;
        }
    }
}
