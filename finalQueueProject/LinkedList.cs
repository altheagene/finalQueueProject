using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalQueueProject
{
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
        int currentNum;

        public LinkedList(int startingNum)
        {
            currentNum = startingNum;
        }



        public int enqueue()
        {
            Node new_node = new Node(currentNum++);
            new_node.priority = false;
            if (size == 0)
            {
                head = new_node;
                tail = new_node;
                size++;
            }
            else
            {
                //Node temp = tail;
                tail.next = new_node;
                tail = new_node;
                tail.priority = false;
                size++;

            }
            return currentNum;
        }

        public int priorityEnqueue()
        {
            Node new_node = new Node(currentNum++);
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

           
            

            //if (size == 0)
            //{
            //    head = new_node;
            //    tail = new_node;
            //    tail.next = null;
            //    size++;
            //}
            //else
            //{
            //    //Node temp = tail;
            //    Node temp = head;
            //    head = new_node;
            //    head.next = temp;

            //}

            return currentNum;
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
            Node temp = head;
            head = temp.next;
            temp.next = null;
            size--;
        }

        public Boolean isEmpty()
        {
            if(size > 0)
            {
                return false;
            }

            return true; // means that queue is empty
        }

        //public void isFull()
    }
}
