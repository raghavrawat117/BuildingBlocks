## Use the attached launch.json to debug the code.

## For your project
Following fields need to be changed :
- `program`
- `cwd`

`program` is the place in the bin where the final .dll file would be created

`cwd` is the place name of directory where .cs proj file will be located.

## About ENV_Dev.cs file
In order for the credentials to not get checked in <br>
an extension of `_Dev` file has been created which <br>
will hold the strcuture of the credentials used <br>
but not the value. <br>

When you will put the value in ENV, code will run but the value won't be taken in repo. <br>

## Use the attached tasks.json to debug the code.
Else it will throw error of not able to find task.

