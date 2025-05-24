using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalQueueProject
{

    //This is a simple queue system for a small bank. The bank has four counters, each with different services.
    //The capacity of the queue for each counter is 50. That is the amount of customers it can accomodate for each counter onsite.
    //The queue inserts a priority customer at the beginning of the queue. If there is already a priority customer,
    //priority customers must line behind the earlier priority customers


    class LinkedList
    {

        class Node
        {
            public Node next;
            public int data;
            public Boolean priority = false;

            public Node(int d)
            {
                this.next = null;
                this.data = d;
            }

        }

        Node head;
        Node tail;
        int size = 0;
        int capacity = 50;
        int currentNum;

        public LinkedList(int startingNum)
        {
            currentNum = startingNum;
        }



        public int enqueue()
        {
            if (currentNum == 1999)
            {
                currentNum = 1000; //ensures that the number does not exceed beyond 1999
            }

            if (size == capacity)
            {
                MessageBox.Show("Queue is full. Please come back later.");
                return -1;
            }
            Node new_node = new Node(++currentNum); 
            new_node.priority = false;
            if (size == 0)
            {
                head = new_node;
                tail = new_node;
                size++;
            }
            else
            {
                tail.next = new_node;
                tail = new_node;
                tail.priority = false;
                size++;

            }
            return new_node.data;
        }

        public int priorityEnqueue()
        {
            if (currentNum == 1999)
            {
                currentNum = 1000; //ensures that the number does not exceed beyond 1999
            }

            if (size == capacity)
            {
                MessageBox.Show("Queue is full. Please come back later.");
                return -1;
            }

            Node new_node = new Node(++currentNum);
            new_node.priority = true;


            if (size == 0)
            {
                head = new_node;
                tail = new_node;
                size++;
            }
            else
            {
                Node temp = head;
                Node prev = head;

                if (temp.priority == true)  //if current node is a priority
                {
                    prev = temp;
                    temp = temp.next;

                    while (temp != null && temp.priority == true) //if current node is still a priority
                    {
                        prev = temp;
                        temp = temp.next;
                    }
                }
                else //if current node is NOT a priority
                {
                    Node temp2 = head;
                    head = new_node;
                    head.next = temp2;
                    size++;
                    return currentNum;
                }

                Node regularNode = temp;
                prev.next = new_node;
                new_node.next = temp ;
                size++;
            }

            return new_node.data;
        }

        public void display()
        {
            Node temp = head;

            while (temp != null)
            {
                Debug.Write(temp.data + " ");
                temp = temp.next;
            }

            Debug.WriteLine(" ");
        }

        public void dequeue()
        {
            if (head == null) return;

            Node temp = head;
            head = temp.next;
            temp.next = null;
            size--;
        }       
    }
}
