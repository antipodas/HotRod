using System;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace HotRod.Test.Pedro
{
    internal class Person : IMessage<Person>
    {
        public int Id;
        public string Name;
        public string Surname;
        private Person person;

        public Person()
        {
            this.Id = -1;
            this.Name = "";
            this.Surname = "";
        }

        public MessageDescriptor Descriptor
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int CalculateSize()
        {
            int num = 0;
            if (this.Id > 0)
            {
                num += 1 + CodedOutputStream.ComputeInt32Size(this.Id);
            }
            if (this.Name.Length > 0)
            {
                num += 1 + CodedOutputStream.ComputeStringSize(this.Name);
            }
            if (this.Surname.Length > 0)
            {
                num += 1 + CodedOutputStream.ComputeStringSize(this.Surname);
            }
            return num;

        }

        public Person Clone()
        {
            throw new NotImplementedException();
        }

        public bool Equals(Person other)
        {
            if (other == null)
            {
                return false;
            }
            if (other != this)
            {
                if (this.Id != other.Id)
                {
                    return false;
                }
                if (this.Name != other.Name)
                {
                    return false;
                }
                if (this.Surname != other.Surname)
                {
                    return false;
                }
            }
            return true;

        }

        public void MergeFrom(CodedInputStream input)
        {
            uint num;
            while ((num = input.ReadTag()) > 0)
            {
                switch (num)
                {
                    case 0x1a:
                        this.Name = input.ReadString();
                        break;

                    case 0x22:
                        this.Surname = input.ReadString();
                        break;

                    case 8:
                        this.Id = input.ReadInt32();
                        break;

                    default:
                        input.SkipLastField();
                        break;
                }
            }
        }

        public void MergeFrom(Person other)
        {
            if (other != null)
            {
                if (other.Id > 0)
                {
                    this.Id = other.Id;
                }
                if (other.Name.Length > 0)
                {
                    this.Name = other.Name;
                }
                if (other.Surname.Length > 0)
                {
                    this.Surname = other.Surname;
                }
            }
        }

        public void WriteTo(CodedOutputStream output)
        {
            if (this.Id > 0)
            {
                output.WriteRawTag(8);
                output.WriteInt32(this.Id);
            }
            if (this.Name.Length > 0)
            {
                output.WriteRawTag(0x1a);
                output.WriteString(this.Name);
            }
            if (this.Surname.Length > 0)
            {
                output.WriteRawTag(0x22);
                output.WriteString(this.Surname);
            }

        }

             public String getName()
        {
            return Name;
        }

        public void setSurname(String surname)
        {
            this.Surname = surname;
        }

        public String getSurname()
        {
            return Surname;
        }

        public void setName(String name)
        {
            this.Name = name;
        }

        public int getId()
        {
            return Id;
        }

        public void setId(int id)
        {
            this.Id = id;
        }

        public String toString()
        {
            return "Person{" +
                  "id=" + Id +
                  ", name='" + Name +
                  "', surname='" + Surname + '\'' +
                  '}';
        }

    }
}