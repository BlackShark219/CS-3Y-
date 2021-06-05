%define SYS_READ 0
%define SYS_WRITE 1
%define SYS_OPEN 2
%define SYS_CLOSE 3
%define SYS_LSEEK 8
%define SYS_MMAP 9
%define SYS_MUNMAP 11
%define SYS_DUP2 33
%define SYS_SOCKET 41
%define SYS_CONNECT 42
%define SYS_ACCEPT 43
%define SYS_BIND 49
%define SYS_LISTEN 50
%define SYS_FORK 57
%define SYS_EXECVE 59
%define SYS_EXIT 60
%define SYS_WAIT4 61

%define STDIN 0
%define STDOUT 1
%define STDERR 2

%define O_RDONLY 0
%define O_WRONLY 1
%define O_RDWR 2
%define O_TRUNC 0x00001000
%define O_APPEND 0x00002000
%define O_DIRECT 0x00040000

%define SEEK_SET 0
%define SEEK_CUR 1
%define SEEK_END 2

%define PROT_READ 1
%define PROT_WRITE 2
%define PROT_EXEC 4

%define MAP_SHARED 1
%define MAP_PRIVATE 2

%define AF_INET 2
%define SOCK_STREAM 1
%define IPPROTO_TCP 6
