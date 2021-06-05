ASFLAGS:= -c -g --gdwarf-2
LDFLAGS:= -static
TARGET:= lab4

all: lab4

%.s.o: %.s defs.h
        as $(ASFLAGS) -o $<.o -c $<
        

lab4: $(TARGET).s.o
        ld $(LDFLAGS) -o $(TARGET) $^

clean: 
        rm -rf $(TARGET)
        rm -rf *.o