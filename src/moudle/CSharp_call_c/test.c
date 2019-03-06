
/* gcc -shared -o test.dll test.c
**允许不安全代码
**dll的路径
**数据类型的长度
*/
void swap(long int *a,long int *b){
	long int c=*a;
	*a=*b;
	*b=c;
}