using UnityEngine;

public interface ICollectableFactory {
  public GameObject Supply();
}

public class GemFactory : ICollectableFactory {

  private GameObject pf;

  public GemFactory ( GameObject pf ) {
    this.pf = pf;
  }

  public GameObject Supply () {
    return pf; }
}

