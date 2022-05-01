# Create table of records type of T

- Implement the ability to write in table form to the text stream a set of elements of type `T` (`ICollection<T>`), where the state of each object of type `T` is described by public properties that have only build-in type (`int`, `char`, `string` etc.).
- The number of columns in the table is determined by the number of public properties of each entry. The width of each column is determined by the maximum number of characters required to write an individual property to the stream. In this case, numbers and dates should be right-aligned, strings and symbols - left-aligned.


    ![](/TableRecords.png)


- Check the operation of the developed functionality on the example of any class that has the set of properties indicated above, using the output to a text file and the console (unit tests).
