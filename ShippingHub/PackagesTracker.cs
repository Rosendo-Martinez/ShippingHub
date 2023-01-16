using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingHub
{
    public class PackagesTracker
    {
        private uint unusedPackageIDNumber;
        private List<Package> packages;
        private const int DEFAULT_PACKAGES_ARRAY_SIZE = 50;
        public int currentPointer;

        public PackagesTracker() { 
            unusedPackageIDNumber = 0;
            currentPointer = -1;
            packages = new List<Package>();
        }

        public uint CurrentID() { 
            return unusedPackageIDNumber;
        }
            
        public void ScanNew(string City, string StateInitials, string Zip, string Address, DateTime date) {
            Package newPackage = new Package() { Address = Address, ID = unusedPackageIDNumber, City = City, StateInitials = StateInitials, Zip = Zip, ArrivalDate = date };
            packages.Add(newPackage);
            unusedPackageIDNumber++;
        } 

        public void Edit(uint ID, string Address, string City, string StateInitials, string Zip) {
            Package packageToEdit = null;
            foreach (Package package in packages) {
                if (package.ID == ID) {
                    packageToEdit = package;
                }
            }
            packageToEdit.City = City;
            packageToEdit.StateInitials = StateInitials;
            packageToEdit.Zip = Zip;
            packageToEdit.Address = Address;
        }

        public void RemovePackage(uint ID) {
            Package toRemove = null;
            foreach (Package package in packages) {
                if (package.ID == ID) {
                    toRemove = package;
                }
            }
            packages.Remove(toRemove);
        }

        public Package GetNext() {
            if (currentPointer < packages.Count - 1 && currentPointer >= -1) {
                currentPointer++;
                return packages[currentPointer];
            } 
            return null;
        }

        public Package GetPrev() {
            if (currentPointer > packages.Count) {
                currentPointer = packages.Count;
            }
            if (currentPointer <= packages.Count && currentPointer > 0) { 
                currentPointer--;
                return packages[currentPointer];
            }
            return null;
        }

        public string GetPackageIDsByState(string State) {
            string str = $"ID's of Packages: {State}\n";
            int count = 1;
            foreach (Package package in packages) {
                if (package.StateInitials.Equals(State)) {
                    str += $"{count}: {package.ID}\n";
                    count++;
                }
            }
            return str;
        }

        public bool Contains(uint ID) {
            foreach (Package package in packages) {
                if (package.ID == ID) {
                    return true;
                }
            }
            return false;
        }

        public void ResetPointer() {
            currentPointer = -1;
        }
    }
}
