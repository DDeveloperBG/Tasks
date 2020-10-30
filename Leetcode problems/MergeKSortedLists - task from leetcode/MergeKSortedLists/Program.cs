using System;
using System.Linq;

namespace MergeKSortedLists
{
    class Program
    {
        static void Main()
        {
            // Example input data [[1, 2, 3, 4], [-4, 0, 4, 5], [2, 4, 5, 6]]
            ListNode node1 = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4))));
            ListNode node2 = new ListNode(-4, new ListNode(0, new ListNode(4, new ListNode(5))));
            ListNode node3 = new ListNode(2, new ListNode(4, new ListNode(5, new ListNode(6))));

            ListNode[] listNodes = new ListNode[3] { node1, node2, node3 };

            ListNode result = MergeKLists(listNodes);

            //Expected result [-4, 0, 1, 2, 2, 3, 4, 4, 4, 5, 5, 6]
            while (result != null)
            {
                Console.Write(result.val + ", ");
                result = result.next;
            }
        }

        /// <summary>
        /// Main method for sorting the array of linked lists
        /// </summary>
        /// <param name="lists">The array of linked lists</param>
        /// <returns>Sorted array, combination of all linked lists</returns>
        static ListNode MergeKLists(ListNode[] lists)
        {
            // Remove empty linked lists
            ListNode[] listNodes = lists.Where(node => node != null).ToArray();

            // Check if the hole array is empty
            if (listNodes.Length == 0 || listNodes == null)
            {
                return null;
            }

            // Main list, which would contain the result
            ListNode sortedlist = listNodes[0];

            // Go through all lists 
            for (int i = 1; i < listNodes.Length; i++)
            {
                SortLinkedLists(sortedlist, listNodes[i]);
            }

            // Return sorted array
            return sortedlist;
        }

        /// <summary>
        /// Recursively goes through all nodes of the second array and compares them with the nodes of the main list and places them correctly on the main list
        /// </summary>
        /// <param name="sortedList">Main list</param>
        /// <param name="otherList">List in the array of linked lists</param>
        static void SortLinkedLists(ListNode sortedList, ListNode otherList)
        {
            // Check to finish the recursion
            if (otherList == null)
            {
                return;
            }

            // Compare nodes
            if (otherList.val >= sortedList.val)
            {
                // Swich places
                sortedList = FindBiggerValue(sortedList, otherList.val);

                ListNode leftPart = otherList.next;

                otherList.next = sortedList.next;
                sortedList.next = otherList;

                otherList = leftPart;
            }
            else
            {
                // Swich places
                int sorteListVal = sortedList.val;

                sortedList.val = otherList.val;
                sortedList.next = new ListNode(sorteListVal, sortedList.next);

                otherList = otherList.next;
            }

            // Check to finish the recursion
            if (sortedList.next == null)
            {
                sortedList.next = otherList;
                return;
            }

            // Continue the recursion
            SortLinkedLists(sortedList, otherList);
        }

        /// <summary>
        /// Recursively finds the closest value to the bigger node in the main list, so that positions won't discrepan
        /// </summary>
        /// <param name="sortedList">Main list</param>
        /// <param name="otherListVal">Bigger node in the second list</param>
        /// <returns>Returns the position of the closest node</returns>
        static ListNode FindBiggerValue(ListNode sortedList, int otherListVal)
        {
            // Check if needed to end the recursion
            if (sortedList.next == null || sortedList.next.val > otherListVal)
            {
                return sortedList;
            }

            // Continue the recursion
            return FindBiggerValue(sortedList.next, otherListVal);
        }
    }

    // Object for linked list
    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
}