int NumOfProcess = 5;
int NumOfResource = 2;

// resources already allocated to process
int[][] Allocated = new int[][]
{
    new int[] {0, 1},
    new int[] {2, 0},
    new int[] {3, 0},
    new int[] {1, 1},
    new int[] {0, 0}
};

// max resources that can be allocated to process
int[][] Maximum = new int[][]
{
    new int[] {7, 5},
    new int[] {3, 2},
    new int[] {9, 0},
    new int[] {2, 2},
    new int[] {4, 3}
};

// total resources 
int[] Total = new int[] {9, 5};

// set availbale to total
int[] Available = new int[2];
for(int i = 0; i < NumOfResource; i++)
    Available[i] = Total[i];

// calculate available resources
for(int i = 0; i < NumOfResource; i++)
    for(int j = 0; j < NumOfProcess; j++)
        Available[i] -= Allocated[j][i];

// calculate the resources each process needs to finish
int[,] Need = new int[5,2];
for(int i = 0; i < NumOfResource; i++)
    for(int j = 0; j < NumOfProcess; j++)
        Need[j, i] = Maximum[j][i] - Allocated[j][i];

// we will put all the process to hashset and keep on executing until this set is empty or we can't proceed
HashSet<int> processes = new HashSet<int>() {0, 1, 2, 3, 4};
bool isSafe = true;
while(processes.Count > 0 && isSafe)
{
    int init = processes.Count;
    foreach(int proc in processes)
    {
        bool canExecute = true;
        for(int i = 0; i < NumOfResource; i++)
        {
            if(Available[i] < Need[proc, i])
            {
                canExecute = false;
                break;
            }
        }
        if(canExecute)
        {
            for(int i = 0; i < NumOfResource; i++)
                Available[i] += Allocated[proc][i];
            processes.Remove(proc);
        }
    }
    if(init == processes.Count)
        isSafe = false;
}
Console.WriteLine(isSafe);