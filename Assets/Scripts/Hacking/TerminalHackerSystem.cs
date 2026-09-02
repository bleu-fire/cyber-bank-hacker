using UnityEngine;
using UnityEngine.UI;
using CyberBank.Environment;

namespace CyberBank.Hacking
{
    public class TerminalHackerSystem : MonoBehaviour
    {
        [Header("Terminal UI References")]
        public GameObject terminalCanvas;
        public Text outputText;
        public InputField inputField;

        [Header("Target & Vault Link")]
        public VaultDoorController targetVaultDoor;
        public string targetName = "MAIN VAULT FIREWALL";
        public string secretPasscode = "CYBER2026";

        private bool isTerminalActive = false;

        public void OnInteract()
        {
            OpenTerminal();
        }

        public void OpenTerminal()
        {
            isTerminalActive = true;
            if (terminalCanvas != null) terminalCanvas.SetActive(true);

            outputText.text = $"[CYBER OS v4.2 - CONNECTED TO {targetName}]\n" +
                              $"SECURITY LEVEL: HIGH\n" +
                              $"HINT: Password Anagram -> '{ScrambleString(secretPasscode)}'\n\n" +
                              $"Type password and press ENTER:";

            if (inputField != null)
            {
                inputField.text = "";
                inputField.ActivateInputField();
            }
        }

        public void OnSubmitInput(string input)
        {
            if (!isTerminalActive) return;

            if (input.Trim().ToUpper() == secretPasscode.ToUpper())
            {
                outputText.text = ">>> ACCESS GRANTED <<<\n" +
                                  "FIREWALL BYPASSED! UNLOCKING VAULT DOORS...\n\n" +
                                  "Press [ESC] to exit terminal.";

                if (targetVaultDoor != null)
                {
                    targetVaultDoor.UnlockVault();
                }
            }
            else if (input.Trim().ToLower() == "exit")
            {
                CloseTerminal();
            }
            else
            {
                outputText.text += $"\n[ACCESS DENIED] Incorrect passcode '{input}'. Try again!";
            }

            if (inputField != null)
            {
                inputField.text = "";
                inputField.ActivateInputField();
            }
        }

        void Update()
        {
            if (isTerminalActive && Input.GetKeyDown(KeyCode.Escape))
            {
                CloseTerminal();
            }
        }

        public void CloseTerminal()
        {
            isTerminalActive = false;
            if (terminalCanvas != null) terminalCanvas.SetActive(false);
        }

        private string ScrambleString(string source)
        {
            char[] chars = source.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                int r = Random.Range(0, chars.Length);
                char tmp = chars[i];
                chars[i] = chars[r];
                chars[r] = tmp;
            }
            return new string(chars);
        }
    }
}
