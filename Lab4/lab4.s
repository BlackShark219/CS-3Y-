.include "defs.h"

.section .bss
envp: .quad 0
argc: .quad 0

.section .text
newline:
.byte '\n'

.global _start

_start:
        movq (%rsp), %rbx	/* rbx = rsp */
		movq %rbx, argc		/* argc = rsp */	
        leaq 24(%rsp), %rcx 
        movq %rcx, envp		/* envp = rcx */

loop:
        movq envp, %rcx		/* rcx = envp */
        movq (%rcx), %rsi	/* rsi = *envp */
        movq %rsi, %rdi		/* rdi = rsi */
        movq $0, %rdx		/* rdx = 0 */

strlen:
        cmpb $0, (%rdi)		/* while (*rsi != '\0') */
        jz cont				/* if cmp=true goes to cont label*/
        incq %rdi			/* rdi++ */
        incq %rdx			/* rdx++ */
        jmp strlen			/*goes to strlen cont label*/

cont:
        movq $SYS_WRITE, %rax	
        movq $STDOUT, %rdi
        syscall

        addq $8, envp		/* envp++ (8 = pointer size) */
        movq envp, %r8		/*r8=envp*/
        cmpq $0x0, (%r8)	/*compare r8 with 0*/
        jz end				/* if cmpq=true goes to end label*/
        cmpq $0,envp		/*compare envp with 0*/
        jz end				/* if cmpq=true goes to end label*/
		/* write newline */
        movq $SYS_WRITE, %rax
        movq $STDOUT, %rdi
        movq $newline, %rsi
        movq $1, %rdx
        syscall

        jmp loop

end:
		/* write newline */
        movq $SYS_WRITE, %rax
        movq $STDOUT, %rdi
        movq $newline, %rsi
        movq $1, %rdx
        syscall

        movq $SYS_EXIT, %rax
        movq $0, %rdi
        syscall