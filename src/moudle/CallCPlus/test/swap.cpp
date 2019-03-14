extern "C" __declspec(dllexport) void swap(int *p,int *q);

void swap (int *p,int *q){
	int temp=*p;
	*p=*q;
	*q=temp;
}
